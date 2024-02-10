using MediatR;
using TypingMaster.Application.Dtos;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Interfaces;

namespace TypingMaster.Application.Functions.Tests.Queries.GetAllTests;
public record GetAllTestsQuery : IRequest<GetAllTestResponse>;

public class GetAllTestResponse : Response<IEnumerable<TypingTestDto>>
{
    private GetAllTestResponse(IEnumerable<TypingTestDto> tests)
    {
        Item = tests;
        Status = ResponseStatus.Success;
    }
    
    private GetAllTestResponse(ResponseStatus status, string message = "")
    {
        Status = status;
        Message = message;
    }

    public static GetAllTestResponse Success(IEnumerable<TypingTestDto> tests) => new(tests);
    public static GetAllTestResponse Failure(ResponseStatus status, string message = "") => new(status, message);
}

public class GetAllTestQueryHandler : IRequestHandler<GetAllTestsQuery, GetAllTestResponse>
{
    private readonly ITypingTestStore _typingTestStore;

    public GetAllTestQueryHandler(ITypingTestStore typingTestStore)
    {
        _typingTestStore = typingTestStore;
    }

    public async Task<GetAllTestResponse> Handle(GetAllTestsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var tests = await _typingTestStore.GetAllAsync();
           return GetAllTestResponse.Success(tests.ToDto());
        }
        catch (Exception e)
        {
            return GetAllTestResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
}

