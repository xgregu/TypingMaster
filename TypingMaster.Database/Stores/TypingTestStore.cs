﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Application.Events;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Stores;

public class TypingTestStore(ILogger<TypingTestStore> logger, IDbContextFactory<TestDbContext> dbFactory)
    : BaseRepository<TypingTestEntity>(logger, dbFactory), ITypingTestStore
{
    
    public async Task<long> GetTestRanking(long testId)
    {
        logger.LogInformation("GetPages | TestId={testId}", testId);

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        var index = dbContext.TypingTestStatistics
            .AsNoTracking()
            .OrderByDescending(x => x.OverallRating)
            .ThenByDescending(x => x.EffectivenessPercentage)
            .ThenByDescending(x => x.ClickPerMinute)
            .ThenByDescending(x => x.TypingTest.Text.Text.Length)
            .Include(typingTestStatisticsEntity => typingTestStatisticsEntity.TypingTest)
            .ToList()
            .FindIndex(x => x.TypingTest.Id == testId);

        return index + 1;
    }

    public async Task<(ICollection<TypingTestEntity> tests, long totalCount)> GetPages(long startIndex, long count)
    {
        logger.LogInformation("GetPages | StartIndex={start}, Count={count}", startIndex, count);

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        var testsQuery = dbContext.TypingTests
            .AsNoTracking()
            .OrderByDescending(x => x.Statistics.OverallRating)
            .ThenByDescending(x => x.Statistics.EffectivenessPercentage)
            .ThenByDescending(x => x.Statistics.ClickPerMinute)
            .ThenByDescending(x => x.Text.Text.Length);

        var totalCount = await testsQuery.CountAsync();

        var tests = await testsQuery
            .Skip((int) startIndex)
            .Take((int) count)
            .ToArrayAsync();

        return (tests, totalCount);
    }

    public async Task<long> GetCount()
    {
        logger.LogInformation("GetCount");

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        return dbContext.TypingTests
            .AsNoTracking()
            .Count();
    }
}