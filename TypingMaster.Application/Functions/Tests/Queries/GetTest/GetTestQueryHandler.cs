using MediatR;
using TypingMaster.Application.Dtos;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Interfaces;

namespace TypingMaster.Application.Functions.Tests.Queries.GetTest;
public record GetTestQuery(long TestId) : IRequest<GetTestResponse>;

public class GetTestResponse : Response<TypingTestDto>
{
    private GetTestResponse(TypingTestDto typingTest)
    {
        Item = typingTest;
        Status = ResponseStatus.Success;
    }
    
    private GetTestResponse(ResponseStatus status, string message = "")
    {
        Status = status;
        Message = message;
    }

    public static GetTestResponse Success(TypingTestDto typingTest) => new(typingTest);
    public static GetTestResponse Failure(ResponseStatus status, string message = "") => new(status, message);
}

public class GetTestQueryHandler : IRequestHandler<GetTestQuery, GetTestResponse>
{
    private readonly ITypingTestStore _typingTestStore;

    public GetTestQueryHandler(ITypingTestStore typingTestStore)
    {
        _typingTestStore = typingTestStore;
    }

    public async Task<GetTestResponse> Handle(GetTestQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var test = await _typingTestStore.GetByIdAsync(request.TestId);
            return test is not null 
                ? GetTestResponse.Success(test.ToDto())
                : GetTestResponse.Failure(ResponseStatus.NotFound);
        }
        catch (Exception e)
        {
            return GetTestResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
}