using MediatR;
using TypingMaster.Application.Dtos;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Interfaces;

namespace TypingMaster.Application.Functions.TypingLevels.Queries.GetAllTypingLevels;
public record GetAllTypingLevelsQuery: IRequest<GetAllTypingLevelsResponse>;

public class GetAllTypingLevelsResponse : Response<IEnumerable<TypingLevelDto>>
{
    private GetAllTypingLevelsResponse(IEnumerable<TypingLevelDto> typingLevels)
    {
        Item = typingLevels;
        Status = ResponseStatus.Success;
    }
    
    private GetAllTypingLevelsResponse(ResponseStatus status, string message = "")
    {
        Status = status;
        Message = message;
    }

    public static GetAllTypingLevelsResponse Success(IEnumerable<TypingLevelDto> typingLevels) => new(typingLevels);
    public static GetAllTypingLevelsResponse Failure(ResponseStatus status, string message = "") => new(status, message);
}

public class GetAllTypingLevelsQueryHandler : IRequestHandler<GetAllTypingLevelsQuery, GetAllTypingLevelsResponse>
{
    private readonly ITypingLevelsStore _typingLevelsStore;

    public GetAllTypingLevelsQueryHandler(ITypingLevelsStore typingLevelsStore)
    {
        _typingLevelsStore = typingLevelsStore;
    }
    
    public async Task<GetAllTypingLevelsResponse> Handle(GetAllTypingLevelsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var typingLevelEntities = await _typingLevelsStore.GetAllAsync();
            return GetAllTypingLevelsResponse.Success(typingLevelEntities.ToDto());
        }
        catch (Exception e)
        {
            return GetAllTypingLevelsResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
}