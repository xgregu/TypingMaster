namespace TypingMaster.Domain.Interfaces;

public interface IAsyncRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(T[] entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}