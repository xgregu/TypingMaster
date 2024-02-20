using TypingMaster.Domain.Entities;

namespace TypingMaster.Application.Interfaces;

public interface ITypingLevelNamesStore : IAsyncRepository<TypingLevelNameEntity>
{
    Task<IReadOnlyList<TypingLevelNameEntity>> GetAllAsync(string cultureCode);
}