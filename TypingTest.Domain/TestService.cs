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
        var clickPerMinute = GetClickPerMinute(test);
        var completionTime = GetCompletionTime(test);
        var testLenght = test.TextToRewritten.Length;
        var mistakes = test.InorrectClicks;
        var points = GetPoints(test, effectiveness, clickPerMinute);
        return new TestStatistic(testLenght, effectiveness, clickPerMinute, completionTime, mistakes, points);
    }

    private static int GetPoints(Test test, int effectiveness, double clickPerMinute)
    {
        var value = effectiveness * clickPerMinute;
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

    private static double GetClickPerMinute(Test test)
    {
        var completionTime = GetCompletionTime(test);
        var clickPerMinute = test.TotalClicks / completionTime.TotalMinutes;
        return Math.Round(clickPerMinute, 2);
    }

    private static int GetEffectiveness(Test test)
    {
        return test.CorrectClicks * 100 / test.TotalClicks;
    }

    private static TimeSpan GetCompletionTime(Test test)
    {
        return test.EndTime - test.StartTime;
    }
}