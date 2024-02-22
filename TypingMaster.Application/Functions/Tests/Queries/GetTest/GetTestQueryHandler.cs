using MediatR;
using TypingMaster.Application.Extensions;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Domain.Interfaces;
using TypingMaster.Shared.Dtos;

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

    public static GetTestResponse Success(TypingTestDto typingTest)
    {
        return new GetTestResponse(typingTest);
    }

    public static GetTestResponse Failure(ResponseStatus status, string message = "")
    {
        return new GetTestResponse(status, message);
    }
}

public class GetTestQueryHandler(ITypingTestStore typingTestStore) : IRequestHandler<GetTestQuery, GetTestResponse>
{
    public async Task<GetTestResponse> Handle(GetTestQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var test = await typingTestStore.GetByIdAsync(request.TestId);
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