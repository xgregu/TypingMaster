﻿using MediatR;
using TypingMaster.Application.Functions.Common;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Application.Functions.Tests.Queries.GetTestRanking;

public record GetTestRankingQuery(long TestId) : IRequest<GetTestRankingResponse>;

public class GetTestRankingResponse : Response<long>
{
    private GetTestRankingResponse(long ranking)
    {
        Item = ranking;
        Status = ResponseStatus.Success;
    }

    private GetTestRankingResponse(ResponseStatus status, string message = "")
    {
        Status = status;
        Message = message;
    }

    public static GetTestRankingResponse Success(long ranking)
    {
        return new GetTestRankingResponse(ranking);
    }

    public static GetTestRankingResponse Failure(ResponseStatus status, string message = "")
    {
        return new GetTestRankingResponse(status, message);
    }
}

public class GetTestRankingHandler
    (ITypingTestStore typingTestStore) : IRequestHandler<GetTestRankingQuery, GetTestRankingResponse>
{
    public async Task<GetTestRankingResponse> Handle(GetTestRankingQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var ranking = await typingTestStore.GetTestRanking(request.TestId);
            return ranking >= 1
                ? GetTestRankingResponse.Success(ranking)
                : GetTestRankingResponse.Failure(ResponseStatus.NotFound);
        }
        catch (Exception e)
        {
            return GetTestRankingResponse.Failure(ResponseStatus.Error, e.Message);
        }
    }
}