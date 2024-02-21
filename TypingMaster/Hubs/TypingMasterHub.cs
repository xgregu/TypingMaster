using MediatR;
using Microsoft.AspNetCore.SignalR;
using TypingMaster.Application.Events;

namespace TypingMaster.Hubs;

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

public class TestUpdatedHandler(IHubContext<TypingMasterHub, ITypingMasterClient> hubContext)
    : INotificationHandler<TestUpdatedEvent>
{
    public Task Handle(TestUpdatedEvent notification, CancellationToken cancellationToken) => hubContext.Clients.All.TestChanged();
}

