using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using WebViewApp.Views;

namespace WebViewApp;

public class ModuleSignalRConnectivity
{
    private readonly HubConnection _connection;
    private readonly ILogger<ModuleSignalRConnectivity> _logger;
    private readonly WebViewWindow _webViewWindow;

    public ModuleSignalRConnectivity(ILogger<ModuleSignalRConnectivity> logger, WebViewWindow webViewWindow)
    {
        _logger = logger;
        _webViewWindow = webViewWindow;
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:51285/BrowserHub")
            .WithAutomaticReconnect()
            .Build();

        RegisterHandlers();
        Task.Factory.StartNew(InitializeHubConnection, TaskCreationOptions.RunContinuationsAsynchronously);
    }

    public bool IsConnected => _connection.State == HubConnectionState.Connected;

    private void RegisterHandlers()
    {
        _connection.Reconnecting += _ =>
        {
            _logger.LogInformation("ModuleSignalRConnectivity | Reconnecting");
            return Task.CompletedTask;
        };
        _connection.Closed += _ =>
        {
            _logger.LogInformation("ModuleSignalRConnectivity | Closed");
            return Task.CompletedTask;
        };
        _connection.Reconnected += _ =>
        {
            _logger.LogInformation("ModuleSignalRConnectivity | Reconnected");
            return Task.CompletedTask;
        };

        _connection.On<string>("Navigate", url =>
        {
            _logger.LogInformation("Requested navigation to {Url}", url);
            _webViewWindow.WebViewSource = new Uri(url);
        });
        
        _connection.On<string>("Title", title =>
        {
            _logger.LogInformation("Requested set title: {Title}", title);
            _webViewWindow.WebViewTitle = title;
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