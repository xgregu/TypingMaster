using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;

namespace TypingMaster.Database.Stores;

public class BaseRepository<T>(ILogger<BaseRepository<T>> logger, IServiceProvider serviceProvider)
    : IAsyncRepository<T>
    where T : class
{
    public virtual async Task<T> AddAsync(T entity)
    {
        logger.LogInformation("AddAsync | {@entity}", entity);
        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task AddRangeAsync(T[] entities)
    {
        logger.LogInformation("AddAsync | {@entities}", entities);
        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        await context.Set<T>().AddRangeAsync(entities);
        await context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        logger.LogInformation("DeleteAsync | {@entity}", entity);
        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync()
    {
        logger.LogInformation("GetAllAsync");
        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        return await context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(long id)
    {
        logger.LogInformation("GetByIdAsync | Id={@id}", id);
        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        return await context.Set<T>().FindAsync(id);
    }

    public virtual async Task UpdateAsync(T entity)
    {
        logger.LogInformation("UpdateAsync | {@entity}", entity);
        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}