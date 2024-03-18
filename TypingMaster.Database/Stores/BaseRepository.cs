using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities.Common;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Stores;

public class BaseRepository<T>(ILogger logger, IDbContextFactory<TestDbContext> dbFactory) : IAsyncRepository<T>
    where T : BaseEntity
{

    public virtual async Task<T> AddAsync(T entity)
    {
        logger.LogInformation("AddAsync | {@entity}", entity);

        await using var dbContext = await dbFactory.CreateDbContextAsync();

        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return await GetByIdAsync(entity.Id);
    }

    public virtual async Task<IReadOnlyList<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        logger.LogInformation("AddAsync | {@entities}", entities);
        await using var dbContext = await dbFactory.CreateDbContextAsync();
        var entitiesToSave = entities.ToList();
        await dbContext.Set<T>().AddRangeAsync(entitiesToSave);
        await dbContext.SaveChangesAsync();
        return entitiesToSave.ToList().AsReadOnly();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        logger.LogInformation("DeleteAsync | {@entity}", entity);

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync()
    {
        logger.LogInformation("GetAllAsync");

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        return await dbContext.Set<T>().AsNoTracking().ToListAsync();
    }
    
    public IQueryable<T> GetAllQuerable(DbContext dbContext)
    {
        logger.LogInformation("GetAllQuerable");
        return dbContext.Set<T>().AsNoTracking().AsQueryable();
    }

    public virtual async Task<T> GetByIdAsync(long id)
    {
        logger.LogInformation("GetByIdAsync | Id={@id}", id);

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        return await dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task UpdateAsync(T entity)
    {
        logger.LogInformation("UpdateAsync | {@entity}", entity);

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }
}