using Humanizer;
using TypingMaster.Domain;
using TypingMaster.Domain.Models;

namespace TypingMaster.Components;

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
    public string? OsName { get; set; }
    public bool? IsDesktop { get; set; }
    public bool? IsMobile { get; set; }
    public bool? IsTablet { get; set; }
    public bool? IsAndroid { get; set; }
    public bool? IsIPhone { get; set; }
    public bool? IsIPad { get; set; }
    public bool? IsIPadPro { get; set; }
}