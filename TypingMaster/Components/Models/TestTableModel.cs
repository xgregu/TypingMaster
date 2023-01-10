using Humanizer;
using TypingMaster.Domain;

namespace TypingMaster.Components.Models;

public class TestTableModel
{
    public Guid Id { get; init; }
    public TypingTestType TestType { get; init; }
    public string TestTypeName => TestType.Humanize();
    public string TextToRewritten { get; init; }
    public string ExecutorName { get; init; }
    public DateTime TestDate { get; init; }
    public int CorrectClicks => TotalClicks - InorrectClicks;
    public int InorrectClicks { get; init; }
    public int TotalClicks { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public int TestLenght { get; init; }
    public int EffectivenessPercentage { get; init; }
    public double ClickPerSecond { get; init; }
    public TimeSpan CompletionTime { get; init; }
    public int Mistakes { get; set; }
    public int Points { get; set; }
}