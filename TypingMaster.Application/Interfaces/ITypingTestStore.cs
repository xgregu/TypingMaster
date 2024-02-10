using TypingMaster.Domain.Entities;

namespace TypingMaster.Application.Interfaces;

public interface ITypingTestStore : IAsyncRepository<TypingTestEntity>
{
    Task<TypingTestEntity> GetLast();
}