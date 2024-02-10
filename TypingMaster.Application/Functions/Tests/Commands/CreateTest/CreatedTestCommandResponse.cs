using TypingMaster.Application.Dtos;
using TypingMaster.Application.Functions.Common;

namespace TypingMaster.Application.Functions.Tests.Commands.CreateTest;

public class CreatedTestCommandResponse : Response<TypingTestDto>
{
    private CreatedTestCommandResponse(TypingTestDto test)
    {
        Item = test;
        Status = ResponseStatus.Success;
    }
    
    private CreatedTestCommandResponse(ResponseStatus status, string message = "")
    {
        Status = status;
        Message = message;
    }

    public static CreatedTestCommandResponse Success(TypingTestDto test) => new(test);
    public static CreatedTestCommandResponse Failure(ResponseStatus status, string message = "") => new(status, message);
}