using TypingMaster.Domain.Entities;

namespace TypingMaster.Domain.Interfaces;

public interface ITypingTestStore : IAsyncRepository<TypingTestEntity>
{
    Task<long> GetTestRanking(long testId);
    Task<(ICollection<TypingTestEntity> tests, long totalCount)> GetPages(long startIndex, long count);
    Task<long> GetCount();
}