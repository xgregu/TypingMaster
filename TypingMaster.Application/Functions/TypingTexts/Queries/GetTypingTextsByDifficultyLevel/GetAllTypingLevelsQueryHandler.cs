﻿using MediatR;
using TypingMaster.Application.Extensions;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Domain.Interfaces;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Application.Functions.TypingTexts.Queries.GetTypingTextsByDifficultyLevel;

public record GetTypingTextsByDifficultyLevelQuery
    (uint DifficultyLevel, string CultureCode) : IRequest<GetTypingTextsByDifficultyLevelResponse>;

public class GetTypingTextsByDifficultyLevelResponse : Response<IEnumerable<TypingTextDto>>
{
    private GetTypingTextsByDifficultyLevelResponse(IEnumerable<TypingTextDto> typingText)
    {
        Item = typingText;
        Status = ResponseStatus.Success;
    }

    private GetTypingTextsByDifficultyLevelResponse(ResponseStatus status, string message = "")
    {
        Status = status;
        Message = message;
    }

    public static GetTypingTextsByDifficultyLevelResponse Success(IEnumerable<TypingTextDto> typingLevels)
    {
        return new GetTypingTextsByDifficultyLevelResponse(typingLevels);
    }

    public static GetTypingTextsByDifficultyLevelResponse Failure(ResponseStatus status, string message = "")
    {
        return new GetTypingTextsByDifficultyLevelResponse(status, message);
    }
}

public class GetTypingTextsByDifficultyLevelQueryHandler(ITypingTextsStore typingTextsStore) : IRequestHandler<
    GetTypingTextsByDifficultyLevelQuery,
    GetTypingTextsByDifficultyLevelResponse>
{
    public async Task<GetTypingTextsByDifficultyLevelResponse> Handle(GetTypingTextsByDifficultyLevelQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var typingTextEntities =
                await typingTextsStore.GetByDifficultyLevelAsync(request.DifficultyLevel, request.CultureCode);

            return GetTypingTextsByDifficultyLevelResponse.Success(typingTextEntities.ToDto());
        }
        catch (Exception e)
        {
            return GetTypingTextsByDifficultyLevelResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
}