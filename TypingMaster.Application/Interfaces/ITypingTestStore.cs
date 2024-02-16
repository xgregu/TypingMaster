using TypingMaster.Domain.Entities;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.Application.Interfaces;

public interface ITypingTestStore : IAsyncRepository<TypingTestEntity>
{
    Task<TypingTestEntity> GetLast();
    Task<long> GetTestRanking(long testId);
    Task<(ICollection<TypingTestEntity> tests, long totalCount)> GetPages(long startIndex, long count);
    Task<long> GetCount();

}