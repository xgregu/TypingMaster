using MediatR;
using TypingMaster.Application.Extensions;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Interfaces;
using TypingMaster.Shared.Dtos;

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

public class GetAllTypingLevelsQueryHandler(ITypingLevelsStore typingLevelsStore) : IRequestHandler<GetAllTypingLevelsQuery, GetAllTypingLevelsResponse>
{
    public async Task<GetAllTypingLevelsResponse> Handle(GetAllTypingLevelsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var typingLevelEntities = await typingLevelsStore.GetAllAsync();
            return GetAllTypingLevelsResponse.Success(typingLevelEntities.ToDto());
        }
        catch (Exception e)
        {
            return GetAllTypingLevelsResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
}