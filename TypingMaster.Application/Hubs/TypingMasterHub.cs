using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace TypingMaster.Application.Hubs;

public class TypingMasterHub(ILogger<TypingMasterHub> logger) : Hub<ITypingMasterClient>
{
    public override async Task OnConnectedAsync()
    {
        logger.LogInformation("Client connected {Id}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        logger.LogInformation(exception, "Client disconnected {Id}", Context.ConnectionId);
        return base.OnDisconnectedAsync(exception);
    }
}