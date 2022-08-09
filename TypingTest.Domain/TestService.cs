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

    public TestStatistic GetTestStatistic(Test test)
    {
        _logger.LogInformation("Generate statistic - Test: {id}", test.Id);

        var effectiveness = GetEffectiveness(test);
        var clickPerSecond = GetClickPerSecond(test);
        var completionTime = GetCompletionTime(test);
        var testLenght = test.TextToRewritten.Length;
        var mistakes = test.InorrectClicks;

        return new TestStatistic(testLenght, effectiveness, clickPerSecond, completionTime, mistakes);
    }

    private double GetClickPerSecond(Test test)
    {
        var completionTime = GetCompletionTime(test);
        var clickPerSecond = test.TotalClicks / completionTime.TotalSeconds;
        return Math.Round(clickPerSecond, 0);
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