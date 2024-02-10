using MediatR;
using TypingMaster.Application.Dtos;

namespace TypingMaster.Application.Functions.Tests.Commands.CreateTest;

public record CreatedTestCommand(TestRequest TestRequest) : IRequest<CreatedTestCommandResponse>;