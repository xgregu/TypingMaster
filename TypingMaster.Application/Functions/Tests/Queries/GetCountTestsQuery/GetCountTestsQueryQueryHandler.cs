using MediatR;
using TypingMaster.Application.Extensions;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Interfaces;
using TypingMaster.Shared.Dtos;

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

    public static GetCountTestsResponse Success(long count) => new(count);
    public static GetCountTestsResponse Failure(ResponseStatus status, string message = "") => new(status, message);
}

public class GetCountTestsQueryHandler(ITypingTestStore typingTestStore) : IRequestHandler<GetCountTestsQuery, GetCountTestsResponse>
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