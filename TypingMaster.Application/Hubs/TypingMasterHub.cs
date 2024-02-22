using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using TypingMaster.Application.Events;

namespace TypingMaster.Application.Hubs;

public class TypingMasterHub(ILogger<TypingMasterHub> logger) : Hub<ITypingMasterClient>, INotificationHandler<TestUpdatedEvent>
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

    public Task Handle(TestUpdatedEvent notification, CancellationToken cancellationToken)
    {
        return Clients.All.TestChanged();
    }
}