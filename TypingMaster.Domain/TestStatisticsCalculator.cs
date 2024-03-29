﻿using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Interfaces;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Domain;

public interface ITestStatisticsCalculator
{
    Task<TypingTestStatisticsEntity> GetTestStatistic(CreateTestRequest createTest);
}

public class TestStatisticsCalculator(ILogger<TestStatisticsCalculator> logger, ITypingTextsStore typingTextsStore)
    : ITestStatisticsCalculator
{
    public async Task<TypingTestStatisticsEntity> GetTestStatistic(CreateTestRequest createTest)
    {
        var textEntity = await typingTextsStore.GetByIdAsync(createTest.TextId);
        var textLenght = textEntity.Text.Length;

        var effectiveness = GetEffectiveness(textLenght, createTest.TotalClicks);
        var clickPerMinute = GetClickPerMinute(createTest);
        var completionTime = GetCompletionTime(createTest);
        var overallRating =
            GetOverallRating(effectiveness, clickPerMinute, textEntity.DifficultyLevel.DifficultyCoefficient);

        return new TypingTestStatisticsEntity
        {
            EffectivenessPercentage = effectiveness,
            ClickPerMinute = clickPerMinute,
            CompletionTimeMilliseconds = (long) completionTime.TotalMilliseconds,
            TotalClicks = createTest.TotalClicks,
            MistakesClicks = createTest.TotalClicks - textLenght,
            OverallRating = overallRating
        };
    }

    private long GetOverallRating(long effectiveness, double clickPerMinute, double difficultyCoefficient)
    {
        var points = effectiveness * clickPerMinute;
        return (long) (points * difficultyCoefficient);
    }

    private double GetClickPerMinute(CreateTestRequest createTest)
    {
        var completionTimeMilliseconds = GetCompletionTime(createTest).TotalMilliseconds;
        var clicksPerMillisecond = createTest.TotalClicks / completionTimeMilliseconds;
        var clicksPerMinute = clicksPerMillisecond * 60000;
        return Math.Round(clicksPerMinute, 2);
    }

    private long GetEffectiveness(long textLenght, long totalCLicks)
    {
        return textLenght * 100 / totalCLicks;
    }

    private TimeSpan GetCompletionTime(CreateTestRequest createTest)
    {
        return createTest.EndTime - createTest.StartTime;
    }
}