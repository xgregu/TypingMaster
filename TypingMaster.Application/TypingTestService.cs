using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Application;

public interface ITestStatisticsCalculator
{
    Task<TypingTestStatisticsEntity> GetTestStatistic(CreateTestRequest createTest);
}

public class TestStatisticsCalculator(ILogger<TestStatisticsCalculator> logger, ITypingTextsStore typingTextsStore)
    : ITestStatisticsCalculator
{
    private readonly ILogger<TestStatisticsCalculator> _logger = logger;

    public async Task<TypingTestStatisticsEntity> GetTestStatistic(CreateTestRequest createTest)
    {
        var textEntity = await typingTextsStore.GetByIdAsync(createTest.TextId);
        var textLenght = textEntity.Text.Length;
        
        var effectiveness = GetEffectiveness(textLenght, createTest.TotalClicks);
        var clickPerMinute = GetClickPerMinute(createTest);
        var completionTime = GetCompletionTime(createTest);
        var overallRating = GetOverallRating(effectiveness, clickPerMinute, textEntity.DifficultyLevel.DifficultyCoefficient);
        
        return new TypingTestStatisticsEntity
        {
            EffectivenessPercentage = effectiveness,
            ClickPerMinute = clickPerMinute,
            CompletionTimeSecond = (long)completionTime.TotalSeconds,
            TotalClicks = createTest.TotalClicks,
            MistakesClicks = createTest.TotalClicks - textLenght,
            OverallRating = overallRating,
        };
    }

    private long GetOverallRating(long effectiveness, double clickPerMinute, double difficultyCoefficient)
    {
        var points = effectiveness * clickPerMinute;
        return(long)(points * difficultyCoefficient);
    }

    private double GetClickPerMinute(CreateTestRequest createTest)
    {
        var completionTime = GetCompletionTime(createTest);
        var clickPerMinute = createTest.TotalClicks / completionTime.TotalMinutes;
        return Math.Round(clickPerMinute, 2);
    }

    private long GetEffectiveness(long textLenght, long totalCLicks) => textLenght * 100 / totalCLicks;
    private TimeSpan GetCompletionTime(CreateTestRequest createTest) => createTest.EndTime - createTest.StartTime;
}