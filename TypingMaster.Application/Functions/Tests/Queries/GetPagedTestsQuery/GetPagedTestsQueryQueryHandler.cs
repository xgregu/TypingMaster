using MediatR;
using TypingMaster.Application.Extensions;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Interfaces;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Application.Functions.Tests.Queries.GetPagedTestsQuery;
public record GetPagedTestsQuery(long StartIndex, long Count) : IRequest<GetPagedTestsResponse>;

public class GetPagedTestsResponse : Response<IEnumerable<TypingTestDto>>
{
    private GetPagedTestsResponse(IEnumerable<TypingTestDto> typingTests)
    {
        Item = typingTests;
        Status = ResponseStatus.Success;
    }
    
    private GetPagedTestsResponse(ResponseStatus status, string message = "")
    {
        Status = status;
        Message = message;
    }

    public static GetPagedTestsResponse Success(IEnumerable<TypingTestDto> typingTests) => new(typingTests);
    public static GetPagedTestsResponse Failure(ResponseStatus status, string message = "") => new(status, message);
}

public class GetPagedTestsQueryHandler(ITypingTestStore typingTestStore) : IRequestHandler<GetPagedTestsQuery, GetPagedTestsResponse>
{
    public async Task<GetPagedTestsResponse> Handle(GetPagedTestsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var tests = await typingTestStore.GetPages(request.StartIndex, request.Count);
            return GetPagedTestsResponse.Success(tests.ToDto());
        }
        catch (Exception e)
        {
            return GetPagedTestsResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
}