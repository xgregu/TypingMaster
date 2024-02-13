using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Stores;

public class TypingTestStore(ILogger<TypingTestStore> logger, IServiceProvider serviceProvider,
        ITypingTextsStore typingTextsStore)
    : BaseRepository<TypingTestEntity>(logger, serviceProvider), ITypingTestStore
{
    private readonly ITypingTextsStore _typingTextsStore = typingTextsStore;

    public async Task<TypingTestEntity> AddAsync(TypingTestEntity entity)
    {
        logger.LogInformation("AddAsync | {@entity}", entity);
        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        await context.AddAsync(entity);
        await context.SaveChangesAsync();

        var savedEntity = await GetByIdAsync(entity.Id);
        return savedEntity;
    }

    public override async Task<IReadOnlyList<TypingTestEntity>> GetAllAsync()
    {
        logger.LogInformation("GetAllAsync");
        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        return await context.TypingTests
            .AsNoTracking()
            .Include(x => x.Text)
            .ThenInclude(x => x.DifficultyLevel)
            .Include(x => x.Statistics)
            .ToListAsync();
    }
    
    public override async Task<TypingTestEntity> GetByIdAsync(long id)
    {
        logger.LogInformation("GetByIdAsync | Id={id}", id);

        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var entity = await context.TypingTests
            .AsNoTracking()
            .Include(x => x.Text)
            .ThenInclude(x => x.DifficultyLevel)
            .Include(x => x.Statistics)
            .FirstOrDefaultAsync(x => x.Id == id);
        return entity;
    }

    public async Task<TypingTestEntity> GetLast()
    {
        logger.LogInformation("GetLast");
        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var entity = await context.TypingTests
            .AsNoTracking()
            .Include(x => x.Text)
            .ThenInclude(x => x.DifficultyLevel)
            .Include(x => x.Statistics)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        return entity;
    }
    
    public async Task<long> GetTestRanking(long testId)
    {
        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var index = context.TypingTestStatistics
            .Include(x => x.TypingTest)
            .AsNoTracking()
            .OrderByDescending(x => x.OverallRating)
            .ToList()
            .FindIndex(x => x.TypingTest.Id == testId);
        
        return index + 1;
    }
    
}