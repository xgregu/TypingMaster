using TypingMaster.Domain.Entities;

namespace TypingMaster.Domain.Interfaces;

public interface ITypingTextsStore : IAsyncRepository<TypingTextEntity>
{
    Task<IReadOnlyList<TypingTextEntity>> GetByDifficultyLevelAsync(uint difficultyLevel, string CultureCode);
}