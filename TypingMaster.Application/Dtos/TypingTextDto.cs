using TypingMaster.Domain.Entities;

namespace TypingMaster.Application.Dtos;

public record TypingTextDto(long Id, string Text, uint DifficultyLevel);

public static class TypingTextDtoExtensions
{
    public static TypingTextDto ToDto(this TypingTextEntity entity) => new(entity.Id, entity.Text, entity.DifficultyLevel.DifficultyLevel);

    public static IEnumerable<TypingTextDto> ToDto(this IEnumerable<TypingTextEntity> entities) => entities.Select(entity => entity.ToDto());
}
