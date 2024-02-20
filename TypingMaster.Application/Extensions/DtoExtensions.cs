using TypingMaster.Domain.Entities;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Application.Extensions;

public static class TypingLevelDtoExtensions
{
    public static TypingLevelDto ToDto(this TypingLevelEntity entity, string levelName)
    {
        return new TypingLevelDto(entity.DifficultyLevel, levelName);
    }
}

public static class TypingTestDtoExtensions
{
    public static TypingTestDto ToDto(this TypingTestEntity entity)
    {
        return new TypingTestDto(entity.Id, entity.ExecutorName, entity.StartTime, entity.EndTime, entity.Text.ToDto(),
            entity.Statistics.ToDto());
    }

    public static IEnumerable<TypingTestDto> ToDto(this IEnumerable<TypingTestEntity> entities)
    {
        return entities.Select(entity => entity.ToDto());
    }
}

public static class TypingTestStatisticsDtoDtoExtensions
{
    public static TypingTestStatisticsDto ToDto(this TypingTestStatisticsEntity entity)
    {
        return new TypingTestStatisticsDto(entity.Id, entity.EffectivenessPercentage, entity.ClickPerMinute,
            entity.CompletionTimeMilliseconds,
            entity.TotalClicks, entity.MistakesClicks, entity.OverallRating);
    }

    public static IEnumerable<TypingTestStatisticsDto> ToDto(this IEnumerable<TypingTestStatisticsEntity> entities)
    {
        return entities.Select(entity => entity.ToDto());
    }
}

public static class TypingTextDtoExtensions
{
    public static TypingTextDto ToDto(this TypingTextEntity entity)
    {
        return new TypingTextDto(entity.Id, entity.Text, entity.DifficultyLevel.DifficultyLevel);
    }

    public static IEnumerable<TypingTextDto> ToDto(this IEnumerable<TypingTextEntity> entities)
    {
        return entities.Select(entity => entity.ToDto());
    }
}