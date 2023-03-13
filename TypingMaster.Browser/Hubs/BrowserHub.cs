using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Events;
using TypingMaster.Shared;

namespace Wkp.Navigation.Hubs;

public class BrowserHub : Hub<IWebAppHubClient>
{
    private readonly ILogger<BrowserHub> _logger;
    private readonly IMediator _mediator;
    private readonly string _url;

    public BrowserHub(ILogger<BrowserHub> logger, IMediator mediator, IConfiguration configuration)
    {
        _logger = logger;
        _mediator = mediator;
        _url = configuration.GetRequiredSection("Urls").Value.Replace("0.0.0.0", "127.0.0.1");
    }

    public override Task OnConnectedAsync()
    {
        _logger.LogInformation("WebViewApp Connected: {ContextConnectionId}", Context.ConnectionId);
        Clients.All.Title(Constants.AppFriendlyName);
        Clients.All.Navigate(_url);
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("WebViewApp disconnected: {ContextConnectionId}", Context.ConnectionId);
        await _mediator.Publish(new WebViewAppDisconected());
        await base.OnDisconnectedAsync(exception);
    }
}