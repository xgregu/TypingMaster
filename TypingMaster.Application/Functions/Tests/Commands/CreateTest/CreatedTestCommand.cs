using MediatR;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Application.Functions.Tests.Commands.CreateTest;

public record CreatedTestCommand(CreateTestRequest CreateTestRequest) : IRequest<CreatedTestCommandResponse>;