using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Models;

namespace TypingMaster.Domain;

public class TestService : ITestService
{
    private readonly ILogger<TestService> _logger;

    public TestService(ILogger<TestService> logger)
    {
        _logger = logger;
    }

    public Test TestInProgressEnd(TestInProgress testInProgress, string executorName)
    {
        return new Test
        {
            Id = Guid.NewGuid(),
            TestType = testInProgress.Type,
            TextToRewritten = testInProgress.TextToRewritten,
            ExecutorName = executorName,
            TestDate = DateTime.Now,
            TotalClicks = testInProgress.TotalClicks,
            EndTime = testInProgress.EndTime,
            StartTime = testInProgress.StartTime,
            InorrectClicks = testInProgress.InorrectClicks
        };
    }

    public TestStatistic GetTestStatistic(Test test)
    {
        var effectiveness = GetEffectiveness(test);
        var clickPerSecond = GetClickPerSecond(test);
        var completionTime = GetCompletionTime(test);
        var testLenght = test.TextToRewritten.Length;
        var mistakes = test.InorrectClicks;
        var overallRating = GetOverallRating(test, effectiveness, clickPerSecond);
        return new TestStatistic(testLenght, effectiveness, clickPerSecond, completionTime, mistakes, overallRating);
    }

    private int GetOverallRating(Test test, int effectiveness, double clickPerSecond)
    {
        var value = (effectiveness * clickPerSecond);
        var multiplier = test.TestType switch
        {
            TypingTestType.Minimalistic => 0.8,
            TypingTestType.Short => 0.9,
            TypingTestType.Average => 1,
            TypingTestType.Long => 1.1,
            TypingTestType.Verylong => 1.2,
            _ => 1
        };

        return (int)(value * multiplier);
    }

    private double GetClickPerSecond(Test test)
    {
        var completionTime = GetCompletionTime(test);
        var clickPerSecond = test.TotalClicks / completionTime.TotalSeconds;
        return Math.Round(clickPerSecond, 2);
    }

    private int GetEffectiveness(Test test)
    {
        return test.CorrectClicks * 100 / test.TotalClicks;
    }

    private TimeSpan GetCompletionTime(Test test)
    {
        return test.EndTime - test.StartTime;
    }
}