using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;

namespace TypingMaster.Database.Stores;

public class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    private readonly ILogger<BaseRepository<T>> _logger;
    private readonly IServiceProvider _serviceProvider;

    public BaseRepository(ILogger<BaseRepository<T>> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        _logger.LogInformation("AddAsync | {@entity}", entity);
        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _logger.LogInformation("DeleteAsync | {@entity}", entity);
        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }

    public virtual async Task<IReadOnlyList<T>> GetAllAsync()
    {
        _logger.LogInformation("GetAllAsync");
        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        return await context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(long id)
    {
        _logger.LogInformation("GetByIdAsync | Id={@id}", id);
        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        return await context.Set<T>().FindAsync(id);
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _logger.LogInformation("UpdateAsync | {@entity}", entity);
        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }
}