using Humanizer;

namespace TypingMaster.Domain.Models;

public class Test
{
    public Guid Id { get; init; }
    public TypingTestType TestType { get; init; }
    public string TestTypeName => TestType.Humanize();
    public string TextToRewritten { get; init; }
    public string ExecutorName { get; init; }
    public DateTime TestDate { get; init; }
    public TestStatistic Statistic { get; init; }
}