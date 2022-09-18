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
    public int CorrectClicks => TotalClicks - InorrectClicks;
    public int InorrectClicks { get; init; }
    public int TotalClicks { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public string? OsName { get; init; }
    public bool? IsDesktop { get; init; }
    public bool? IsMobile { get; init; }
    public bool? IsTablet { get; init; }
    public bool? IsAndroid { get; init; }
    public bool? IsIPhone { get; init; }
    public bool? IsIPad { get; init; }
    public bool? IsIPadPro { get; init; }
}