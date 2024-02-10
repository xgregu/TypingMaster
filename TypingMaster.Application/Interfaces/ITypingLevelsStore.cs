using TypingMaster.Domain.Entities;

namespace TypingMaster.Application.Interfaces;

public interface ITypingLevelsStore : IAsyncRepository<TypingLevelEntity>
{
    Task<TypingLevelEntity> GetByLevelAsync(long level);
}