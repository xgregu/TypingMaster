using Microsoft.EntityFrameworkCore;
using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Domain.Interfaces;

public interface IAsyncRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(long id);
    Task<IReadOnlyList<T>> GetAllAsync();
    IQueryable<T> GetAllQuerable(DbContext dbContext);
    Task<T> AddAsync(T entity);
    Task<IReadOnlyList<T>> AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}