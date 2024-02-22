using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Stores;

public class TypingTextsStore(ILogger<TypingTextsStore> logger, IDbContextFactory<TestDbContext> dbFactory)
    : BaseRepository<TypingTextEntity>(logger, dbFactory), ITypingTextsStore
{
    public override async Task<TypingTextEntity> GetByIdAsync(long id)
    {
        logger.LogInformation("GetByIdAsync | Id={id}", id);

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        var entity = await dbContext.TypingTexts
            .AsNoTracking()
            .Include(x => x.DifficultyLevel)
            .FirstOrDefaultAsync(x => x.Id == id);
        return entity;
    }

    public async Task<IReadOnlyList<TypingTextEntity>> GetByDifficultyLevelAsync(uint difficultyLevel,
        string cultureCode)
    {
        logger.LogInformation("GetByDifficultyLevelAsync | DifficultyLevel={difficultyLevel}", difficultyLevel);

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        return await dbContext.TypingTexts
            .AsNoTracking()
            .Include(x => x.Culture)
            .Include(x => x.DifficultyLevel)
            .Where(x =>
                x.DifficultyLevel.DifficultyLevel == difficultyLevel &&
                EF.Functions.Like(x.Culture.CultureCode, cultureCode))
            .ToListAsync();
    }
}