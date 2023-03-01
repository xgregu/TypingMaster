using TypingMaster.Domain.Models;

namespace TypingMaster.Domain.Extensions;

public static class TestInProgressExtensions
{
    public static Test EndTest(this TestInProgress testInProgress, string executorName)
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
}