using MediatR;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Application.Interfaces;

namespace TypingMaster.Application.Functions.TypingLevels.Queries.GetTypingLevelName;

public record GetTypingLevelNameQuery(uint DifficultyLevel, string CultureCode) : IRequest<GetTypingLevelNameResponse>;

public class GetTypingLevelNameResponse : Response<string>
{
    private GetTypingLevelNameResponse(string levelName)
    {
        Item = levelName;
        Status = ResponseStatus.Success;
    }

    private GetTypingLevelNameResponse(ResponseStatus status, string message = "")
    {
        Status = status;
        Message = message;
    }

    public static GetTypingLevelNameResponse Success(string levelName)
    {
        return new GetTypingLevelNameResponse(levelName);
    }

    public static GetTypingLevelNameResponse Failure(ResponseStatus status, string message = "")
    {
        return new GetTypingLevelNameResponse(status, message);
    }
}

public class GetAllTypingLevelsQueryHandler
    (ITypingLevelNamesStore typingLevelNamesStore) : IRequestHandler<GetTypingLevelNameQuery,
        GetTypingLevelNameResponse>
{
    public async Task<GetTypingLevelNameResponse> Handle(GetTypingLevelNameQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var typingLevelNamesEntities = await typingLevelNamesStore.GetAllAsync(request.CultureCode);
            var levelName = typingLevelNamesEntities.FirstOrDefault(x =>
                x.TypingLevel.DifficultyLevel == request.DifficultyLevel &&
                x.Culture.CultureCode == request.CultureCode);

            return levelName is not null
                ? GetTypingLevelNameResponse.Success(levelName.Name)
                : GetTypingLevelNameResponse.Failure(ResponseStatus.NotFound);
        }
        catch (Exception e)
        {
            return GetTypingLevelNameResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
}