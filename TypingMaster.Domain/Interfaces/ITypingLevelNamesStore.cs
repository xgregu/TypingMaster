using TypingMaster.Domain.Entities;

namespace TypingMaster.Domain.Interfaces;

public interface ITypingLevelNamesStore : IAsyncRepository<TypingLevelNameEntity>
{
    Task<IReadOnlyList<TypingLevelNameEntity>> GetAllAsync(string cultureCode);
}