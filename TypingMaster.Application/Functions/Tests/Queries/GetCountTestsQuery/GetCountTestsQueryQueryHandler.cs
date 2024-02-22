using MediatR;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Application.Functions.Tests.Queries.GetCountTestsQuery;

public record GetCountTestsQuery : IRequest<GetCountTestsResponse>;

public class GetCountTestsResponse : Response<long>
{
    private GetCountTestsResponse(long count)
    {
        Item = count;
        Status = ResponseStatus.Success;
    }

    private GetCountTestsResponse(ResponseStatus status, string message = "")
    {
        Status = status;
        Message = message;
    }

    public static GetCountTestsResponse Success(long count)
    {
        return new GetCountTestsResponse(count);
    }

    public static GetCountTestsResponse Failure(ResponseStatus status, string message = "")
    {
        return new GetCountTestsResponse(status, message);
    }
}

public class GetCountTestsQueryHandler
    (ITypingTestStore typingTestStore) : IRequestHandler<GetCountTestsQuery, GetCountTestsResponse>
{
    public async Task<GetCountTestsResponse> Handle(GetCountTestsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var count = await typingTestStore.GetCount();
            return GetCountTestsResponse.Success(count);
        }
        catch (Exception e)
        {
            return GetCountTestsResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
}