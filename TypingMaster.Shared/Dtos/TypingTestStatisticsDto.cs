namespace TypingMaster.Shared.Dtos;

public record TypingTestStatisticsDto(
    long Id,
    long EffectivenessPercentage,
    double ClickPerMinute,
    long CompletionTimeMilliseconds,
    long TotalClicks,
    long MistakesClicks,
    long OverallRating
);

