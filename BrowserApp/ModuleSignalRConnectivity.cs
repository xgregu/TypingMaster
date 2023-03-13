using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace BrowserApp;

public class ModuleSignalRConnectivity
{
    private readonly IBrowserManager _browserManager;
    private readonly HubConnection _connection;
    private readonly ILogger<ModuleSignalRConnectivity> _logger;

    public ModuleSignalRConnectivity(ILogger<ModuleSignalRConnectivity> logger, IBrowserManager browserManager)
    {
        _logger = logger;
        _browserManager = browserManager;
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:51285/BrowserHub")
            .WithAutomaticReconnect()
            .Build();

        RegisterHandlers();

        _ = Task.Run(InitializeHubConnection);
    }

    public bool IsConnected => _connection.State == HubConnectionState.Connected;

    private void RegisterHandlers()
    {
        _connection.Reconnecting += _ =>
        {
            _logger.LogInformation("{ConnectionId} | Reconnecting", _connection.ConnectionId);
            return Task.CompletedTask;
        };
        _connection.Closed += _ =>
        {
            _logger.LogInformation("{ConnectionId} | Closed", _connection.ConnectionId);
            return Task.CompletedTask;
        };
        _connection.Reconnected += _ =>
        {
            _logger.LogInformation("{ConnectionId} | Reconnected", _connection.ConnectionId);
            return Task.CompletedTask;
        };

        _connection.On<string>("Navigate", url =>
        {
            _logger.LogInformation("{ConnectionId} | Requested navigation to {Url}", _connection.ConnectionId, url);
            _browserManager.NavigateTo(url);
        });

        _connection.On<string>("Title", title =>
        {
            _logger.LogInformation("{ConnectionId} | Requested set title: {Title}", _connection.ConnectionId, title);
            _browserManager.SetTitle(title);
        });
    }

    private async Task InitializeHubConnection()
    {
        _logger.LogInformation("ModuleSignalRConnectivity | InitializeHubConnection");

        if (_connection.State == HubConnectionState.Connected)
            return;

        do
        {
            try
            {
                _logger.LogInformation("Try connection to module");
                await _connection.StartAsync();
                _logger.LogInformation("Connected");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Connection to module failed: {ExMessage}", ex.Message);
            }
        } while (_connection.State != HubConnectionState.Connected);
    }
}