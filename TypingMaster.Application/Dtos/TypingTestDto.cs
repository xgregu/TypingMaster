using TypingMaster.Domain.Entities;

namespace TypingMaster.Application.Dtos;

public record TypingTestDto(
    long Id,
    string ExecutorName,
    DateTimeOffset StartTime,
    DateTimeOffset EndTime,
    TypingTextDto Text,
    TypingTestStatisticsDto Statistics);

public static class TypingTestDtoExtensions
{
    public static TypingTestDto ToDto(this TypingTestEntity entity) =>
        new(entity.Id, entity.ExecutorName, entity.StartTime, entity.EndTime, entity.Text.ToDto(), entity.Statistics.ToDto());

    public static IEnumerable<TypingTestDto> ToDto(this IEnumerable<TypingTestEntity> entities) =>
        entities.Select(entity => entity.ToDto());
}