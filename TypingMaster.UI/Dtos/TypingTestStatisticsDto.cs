namespace TypingMaster.UI.Dtos;

public record TypingTestStatisticsDto(
    long Id,
    long EffectivenessPercentage,
    double ClickPerSecond,
    long CompletionTimeSecond,
    long TotalClicks,
    long MistakesClicks,
    long OverallRating
);