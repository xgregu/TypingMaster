using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Stores;

public class TypingLevelsStore(ILogger<TypingLevelsStore> logger, IServiceProvider serviceProvider)
    : BaseRepository<TypingLevelEntity>(logger, serviceProvider), ITypingLevelsStore
{
    public async Task<TypingLevelEntity> GetByLevelAsync(long level)
    {
        logger.LogInformation("GetByLevelAsync | Level={level}", level);

        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var entity = await context.TypingLevels
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DifficultyLevel == level);
        return entity;
    }
}