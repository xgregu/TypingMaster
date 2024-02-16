using MediatR;
using TypingMaster.Application.Extensions;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Interfaces;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Application.Functions.Tests.Queries.GetPagedTestsQuery;
public record GetPagedTestsQuery(long StartIndex, long Count) : IRequest<GetPagedTestsResponse>;

public class GetPagedTestsResponse : Response<PagedTestResponse>
{
    private GetPagedTestsResponse(IEnumerable<TypingTestDto> typingTests, long totalCount)
    {
        Item = new PagedTestResponse(typingTests.ToArray(), totalCount);
        Status = ResponseStatus.Success;
    }
    
    private GetPagedTestsResponse(ResponseStatus status, string message = "")
    {
        Status = status;
        Message = message;
    }

    public static GetPagedTestsResponse Success(IEnumerable<TypingTestDto> typingTests, long totalCount) => new(typingTests, totalCount);
    public static GetPagedTestsResponse Failure(ResponseStatus status, string message = "") => new(status, message);
}

public class GetPagedTestsQueryHandler(ITypingTestStore typingTestStore) : IRequestHandler<GetPagedTestsQuery, GetPagedTestsResponse>
{
    public async Task<GetPagedTestsResponse> Handle(GetPagedTestsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var paged = await typingTestStore.GetPages(request.StartIndex, request.Count);
            return GetPagedTestsResponse.Success(paged.tests.ToDto(), paged.totalCount);
        }
        catch (Exception e)
        {
            return GetPagedTestsResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
}