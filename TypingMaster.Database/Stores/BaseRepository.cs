﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities.Common;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Stores;

public class BaseRepository<T>(ILogger<BaseRepository<T>> logger, IDbContextFactory<TestDbContext> dbFactory)
    : IAsyncRepository<T> where T : BaseEntity
{
    public async Task<T> AddAsync(T entity)
    {
        logger.LogInformation("AddAsync | {@entity}", entity);

        await using var dbContext = await dbFactory.CreateDbContextAsync();

        await dbContext.Set<T>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        logger.LogInformation("AddAsync | {@entities}", entities);

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        await dbContext.Set<T>().AddRangeAsync(entities);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        logger.LogInformation("DeleteAsync | {@entity}", entity);

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        logger.LogInformation("GetAllAsync");

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(long id)
    {
        logger.LogInformation("GetByIdAsync | Id={@id}", id);

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task UpdateAsync(T entity)
    {
        logger.LogInformation("UpdateAsync | {@entity}", entity);

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }
}