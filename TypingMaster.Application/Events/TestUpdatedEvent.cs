using MediatR;

namespace TypingMaster.Application.Events;

public record TestUpdatedEvent : RecordEventBase, INotification;