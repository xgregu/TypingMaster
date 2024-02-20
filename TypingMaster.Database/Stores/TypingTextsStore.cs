using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Stores;

public class TypingTextsStore(ILogger<TypingTextsStore> logger, IServiceProvider serviceProvider)
    : BaseRepository<TypingTextEntity>(logger,
        serviceProvider), ITypingTextsStore
{
    public override async Task<TypingTextEntity> GetByIdAsync(long id)
    {
        logger.LogInformation("GetByIdAsync | Id={id}", id);

        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var entity = await context.TypingTexts
            .AsNoTracking()
            .Include(x => x.DifficultyLevel)
            .FirstOrDefaultAsync(x => x.Id == id);
        return entity;
    }

    public async Task<IReadOnlyList<TypingTextEntity>> GetByDifficultyLevelAsync(uint difficultyLevel, string cultureCode)
    {
        logger.LogInformation("GetByDifficultyLevelAsync | DifficultyLevel={difficultyLevel}", difficultyLevel);

        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        return await context.TypingTexts
            .AsNoTracking()
            .Include(x => x.Culture)
            .Include(x => x.DifficultyLevel)
            .Where(x => x.DifficultyLevel.DifficultyLevel == difficultyLevel && x.Culture.CultureCode == cultureCode)
            .ToListAsync();
    }
}