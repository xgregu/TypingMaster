using MediatR;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Interfaces;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Application.Functions.TypingLevels.Queries.GetAllTypingLevels;

public record GetAllTypingLevelsQuery(string CultureCode) : IRequest<GetAllTypingLevelsResponse>;

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

    public static GetAllTypingLevelsResponse Success(IEnumerable<TypingLevelDto> typingLevels)
    {
        return new GetAllTypingLevelsResponse(typingLevels);
    }

    public static GetAllTypingLevelsResponse Failure(ResponseStatus status, string message = "")
    {
        return new GetAllTypingLevelsResponse(status, message);
    }
}

public class GetAllTypingLevelsQueryHandler(ITypingLevelsStore typingLevelsStore,
    ITypingLevelNamesStore typingLevelNamesStore) : IRequestHandler<GetAllTypingLevelsQuery, GetAllTypingLevelsResponse>
{
    public async Task<GetAllTypingLevelsResponse> Handle(GetAllTypingLevelsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var typingLevelEntities = await typingLevelsStore.GetAllAsync();
            var typingLevelNamesEntities = await typingLevelNamesStore.GetAllAsync(request.CultureCode);

            var typingLevelDtos = typingLevelEntities.Select(x =>
            {
                var levelName = typingLevelNamesEntities.First(y => x.DifficultyLevel == y.TypingLevel.DifficultyLevel);
                return new TypingLevelDto(x.DifficultyLevel, levelName.Name);
            });

            return GetAllTypingLevelsResponse.Success(typingLevelDtos);
        }
        catch (Exception e)
        {
            return GetAllTypingLevelsResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
}