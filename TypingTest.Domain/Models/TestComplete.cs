namespace TypingMaster.Domain.Models;

public class TestComplete
{
    public Guid Id { get; init; }
    public TypingTestType TestType { get; init; }
    public string Text { get; init; }
    public string ExecutorName { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public int CorrectClicks { get; init; }
    public int InorrectClicks { get; init; }
}