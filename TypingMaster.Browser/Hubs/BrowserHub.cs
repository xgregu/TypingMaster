using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TypingMaster.Shared;

namespace TypingMaster.Browser.Hubs;

public class BrowserHub : Hub<IWebAppHubClient>
{
    private readonly ILogger<BrowserHub> _logger;
    private readonly string _url;

    public BrowserHub(ILogger<BrowserHub> logger, IConfiguration configuration)
    {
        _logger = logger;
        _url = configuration.GetRequiredSection("Urls").Value.Replace("0.0.0.0", "127.0.0.1");
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("BrowserApp Connected: {ContextConnectionId}", Context.ConnectionId);

        Clients.All.Title(Constants.AppFriendlyName);
        Clients.All.Navigate(_url);

        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("BrowserApp disconnected: {ContextConnectionId}", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}