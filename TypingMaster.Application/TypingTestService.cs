using Microsoft.Extensions.Logging;
using TypingMaster.Application.Dtos;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Application;

public interface ITestStatisticsCalculator
{
    Task<TypingTestStatisticsEntity> GetTestStatistic(TestRequest test);
}

public class TestStatisticsCalculator : ITestStatisticsCalculator
{
    private readonly ILogger<TestStatisticsCalculator> _logger;
    private readonly ITypingTextsStore _typingTextsStore;

    public TestStatisticsCalculator(ILogger<TestStatisticsCalculator> logger, ITypingTextsStore typingTextsStore)
    {
        _logger = logger;
        _typingTextsStore = typingTextsStore;
    }
    
    public async Task<TypingTestStatisticsEntity> GetTestStatistic(TestRequest test)
    {
        var textEntity = await _typingTextsStore.GetByIdAsync(test.TextId);
        var textLenght = textEntity.Text.Length;
        
        
        var effectiveness = GetEffectiveness(textLenght, test.TotalClicks);
        var clickPerMinute = GetClickPerMinute(test);
        var completionTime = GetCompletionTime(test);
        var overallRating = GetOverallRating(effectiveness, clickPerMinute, textEntity.DifficultyLevel.DifficultyCoefficient);
        
        return new TypingTestStatisticsEntity
        {
            EffectivenessPercentage = effectiveness,
            ClickPerMinute = clickPerMinute,
            CompletionTimeSecond = completionTime.Seconds,
            TotalClicks = test.TotalClicks,
            MistakesClicks = test.TotalClicks - textLenght,
            OverallRating = overallRating,
        };
    }

    private long GetOverallRating(long effectiveness, double clickPerMinute, double difficultyCoefficient)
    {
        var points = effectiveness * clickPerMinute;
        return(long)(points * difficultyCoefficient);
    }

    private double GetClickPerMinute(TestRequest test)
    {
        var completionTime = GetCompletionTime(test);
        var clickPerMinute = test.TotalClicks / completionTime.TotalMinutes;
        return Math.Round(clickPerMinute, 2);
    }

    private long GetEffectiveness(long textLenght, long totalCLicks) => textLenght * 100 / totalCLicks;
    private TimeSpan GetCompletionTime(TestRequest test) => test.EndTime - test.StartTime;
}