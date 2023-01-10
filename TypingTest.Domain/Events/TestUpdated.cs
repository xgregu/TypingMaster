using MediatR;
using TypingMaster.Domain.Models;

namespace TypingMaster.Domain.Events;

public record TestUpdated(Test Test) : INotification;