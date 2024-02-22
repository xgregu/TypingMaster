using MediatR;
using Microsoft.AspNetCore.SignalR;
using TypingMaster.Application.Events;
using TypingMaster.Application.Hubs;

namespace TypingMaster.Application.Handlers;

public class TestUpdatedHandler(IHubContext<TypingMasterHub, ITypingMasterClient> hubContext)
    : INotificationHandler<TestUpdatedEvent>
{
    public Task Handle(TestUpdatedEvent notification, CancellationToken cancellationToken) => hubContext.Clients.All.TestChanged();
}