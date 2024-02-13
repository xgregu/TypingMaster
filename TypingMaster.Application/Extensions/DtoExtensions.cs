using TypingMaster.Domain.Entities;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Application.Extensions;

public static class TypingLevelDtoExtensions
{
    public static TypingLevelDto ToDto(this TypingLevelEntity entity) =>
        new(entity.Id, entity.Name, entity.DifficultyLevel);
    public static IEnumerable<TypingLevelDto> ToDto(this IEnumerable<TypingLevelEntity> entities) => entities.Select(entity => entity.ToDto());
}

public static class TypingTestDtoExtensions
{
    public static TypingTestDto ToDto(this TypingTestEntity entity) =>
        new(entity.Id, entity.ExecutorName, entity.StartTime, entity.EndTime, entity.Text.ToDto(), entity.Statistics.ToDto());

    public static IEnumerable<TypingTestDto> ToDto(this IEnumerable<TypingTestEntity> entities) =>
        entities.Select(entity => entity.ToDto());
}

public static class TypingTestStatisticsDtoDtoExtensions
{
    public static TypingTestStatisticsDto ToDto(this TypingTestStatisticsEntity entity) =>
        new(entity.Id, entity.EffectivenessPercentage, entity.ClickPerMinute, entity.CompletionTimeMilliseconds, entity.TotalClicks, entity.MistakesClicks, entity.OverallRating);

    public static IEnumerable<TypingTestStatisticsDto> ToDto(this IEnumerable<TypingTestStatisticsEntity> entities) =>
        entities.Select(entity => entity.ToDto());
}

public static class TypingTextDtoExtensions
{
    public static TypingTextDto ToDto(this TypingTextEntity entity) => new(entity.Id, entity.Text, entity.DifficultyLevel.ToDto());
    public static IEnumerable<TypingTextDto> ToDto(this IEnumerable<TypingTextEntity> entities) => entities.Select(entity => entity.ToDto());
}
