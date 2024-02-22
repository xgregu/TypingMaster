using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Domain.Interfaces;

public interface IAsyncRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(long id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}