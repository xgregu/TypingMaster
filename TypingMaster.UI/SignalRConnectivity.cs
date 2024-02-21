using Easy.MessageHub;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using TypingMaster.UI.Events;

namespace TypingMaster.UI;

public class SignalRConnectivity : IHostedService
{
    private readonly ILogger<SignalRConnectivity> _logger;
    private readonly IMessageHub _messageHub;
    private readonly HubConnection _connection;

    public SignalRConnectivity(ILogger<SignalRConnectivity> logger, IMessageHub messageHub, IOptions<BackendSettings> backedSettings)
    {
        _logger = logger;
        _messageHub = messageHub;

        _connection = new HubConnectionBuilder()
            .WithUrl(backedSettings.Value.Gateway + "/hub/")
            .WithAutomaticReconnect()
            .Build();
        
        RegisterHandlers();
    }

    public bool IsConnected => _connection.State == HubConnectionState.Connected;
    
    private void RegisterHandlers()
    {
        _connection.Reconnecting += exception =>
        {
            _logger.LogInformation(exception, "SignalR | Reconnecting");
            _messageHub.Publish(new BackendConnectionStateChanged(IsConnected));
            return Task.CompletedTask;
        };
        
        _connection.Closed += exception =>
        {
            _logger.LogInformation(exception, "SignalR | Closed");
            _messageHub.Publish(new BackendConnectionStateChanged(IsConnected));
            return Task.CompletedTask;
        };
        
        _connection.Reconnected += connectionId =>
        {
            _logger.LogInformation("SignalR | Reconnected | {id}", connectionId);
            _messageHub.Publish(new BackendConnectionStateChanged(IsConnected));
            return Task.CompletedTask;
        };
        
        _connection.On("TestChanged", () =>
            {
                _logger.LogInformation("Module request - TestChanged");
                _messageHub.Publish(new TestUpdatedEvent());
            });
    }
    
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _ = InitializeConnection(cancellationToken);
        return Task.CompletedTask;
    }

    private async Task InitializeConnection(CancellationToken cancellationToken)
    {
        _messageHub.Publish(new BackendConnectionStateChanged(IsConnected));
        
        while (!IsConnected && !cancellationToken.IsCancellationRequested)
        {
            try
            {
                _logger.LogInformation("Try connection to server");
                await _connection.StartAsync(cancellationToken);
                _logger.LogInformation("Connected");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Connection to server failed: {ExMessage}", ex.Message);
            }
        }
        
        _messageHub.Publish(new BackendConnectionStateChanged(true));
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

}