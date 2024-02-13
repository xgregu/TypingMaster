using TypingMaster.Shared.Dtos;

namespace TypingMaster.UI.Components.Models;

public class TestTableModel
{
    public long Id { get; init; }
    public string ExecutorName { get; init; }
    public TypingTextDto Text { get; init; }
    public long InorrectClicks { get; init; }
    public long TotalClicks { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public int TestLenght { get; init; }
    public long EffectivenessPercentage { get; init; }
    public double ClickPerinute { get; init; }
    public TimeSpan CompletionTime { get; init; }
    public long Mistakes { get; set; }
    public long Points { get; set; }

    public static TestTableModel WithTypingTestDto(TypingTestDto typingTestDto)
    {
        return new TestTableModel
        {
            Id = typingTestDto.Id,
            Text = typingTestDto.Text,
            ExecutorName = typingTestDto.ExecutorName,
            TotalClicks = typingTestDto.Statistics.TotalClicks,
            StartTime = typingTestDto.StartTime.DateTime,
            EndTime = typingTestDto.EndTime.DateTime,
            TestLenght = typingTestDto.Text.Text.Length,
            EffectivenessPercentage = typingTestDto.Statistics.EffectivenessPercentage,
            ClickPerinute = typingTestDto.Statistics.ClickPerMinute,
            CompletionTime = TimeSpan.FromSeconds(typingTestDto.Statistics.CompletionTimeMilliseconds),
            Mistakes = typingTestDto.Statistics.MistakesClicks,
            Points = typingTestDto.Statistics.OverallRating
        };
    }
}