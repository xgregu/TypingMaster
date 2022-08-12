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

    public Test TestInProgressEnd(TestInProgress testInProgress)
    {
        return new Test
        {
            Id = Guid.NewGuid(),
            TestType = testInProgress.Type,
            TextToRewritten = testInProgress.TextToRewritten,
            ExecutorName = testInProgress.ExecutorName,
            Statistic = GetTestStatistic(testInProgress),
            TestDate = DateTime.Now
        };
    }

    private TestStatistic GetTestStatistic(TestInProgress testInProgress)
    {
        var effectiveness = GetEffectiveness(testInProgress);
        var clickPerSecond = GetClickPerSecond(testInProgress);
        var completionTime = GetCompletionTime(testInProgress);
        var testLenght = testInProgress.TextToRewritten.Length;
        var mistakes = testInProgress.InorrectClicks;

        return new TestStatistic(testLenght, effectiveness, clickPerSecond, completionTime, mistakes);
    }

    private double GetClickPerSecond(TestInProgress test)
    {
        var completionTime = GetCompletionTime(test);
        var clickPerSecond = test.TotalClicks / completionTime.TotalSeconds;
        return Math.Round(clickPerSecond, 0);
    }

    private int GetEffectiveness(TestInProgress test)
    {
        return test.CorrectClicks * 100 / test.TotalClicks;
    }

    private TimeSpan GetCompletionTime(TestInProgress test)
    {
        return test.EndTime - test.StartTime;
    }
}