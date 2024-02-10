using TypingMaster.Domain.Entities;

namespace TypingMaster.Application.Dtos;

public record TypingTestStatisticsDto(
    long Id,
    long EffectivenessPercentage,
    double ClickPerSecond,
    long CompletionTimeSecond,
    long TotalClicks,
    long MistakesClicks,
    long OverallRating
);

public static class TypingTestStatisticsDtoDtoExtensions
{
    public static TypingTestStatisticsDto ToDto(this TypingTestStatisticsEntity entity) =>
        new(entity.Id, entity.EffectivenessPercentage, entity.ClickPerMinute, entity.CompletionTimeSecond, entity.TotalClicks, entity.MistakesClicks, entity.OverallRating);

    public static IEnumerable<TypingTestStatisticsDto> ToDto(this IEnumerable<TypingTestStatisticsEntity> entities) =>
        entities.Select(entity => entity.ToDto());
}