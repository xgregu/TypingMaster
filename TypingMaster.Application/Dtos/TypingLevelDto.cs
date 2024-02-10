using TypingMaster.Domain.Entities;

namespace TypingMaster.Application.Dtos;

public record TypingLevelDto(long Id, string Name, uint DifficultyLevel);

public static class TypingLevelDtoExtensions
{
    public static TypingLevelDto ToDto(this TypingLevelEntity entity) =>
        new(entity.Id, entity.Name, entity.DifficultyLevel);
    public static IEnumerable<TypingLevelDto> ToDto(this IEnumerable<TypingLevelEntity> entities) => entities.Select(entity => entity.ToDto());
}
