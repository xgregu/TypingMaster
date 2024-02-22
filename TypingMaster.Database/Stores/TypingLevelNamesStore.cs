using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Stores;

public class TypingLevelNamesStore(ILogger<TypingLevelNamesStore> logger, IDbContextFactory<TestDbContext> dbFactory)
    : BaseRepository<TypingLevelNameEntity>(logger, dbFactory), ITypingLevelNamesStore
{
    public async Task<IReadOnlyList<TypingLevelNameEntity>> GetAllAsync(string cultureCode)
    {
        logger.LogInformation("GetAllAsync");

        await using var dbContext = await dbFactory.CreateDbContextAsync();
        return await dbContext.TypingLevelName
            .AsNoTracking()
            .Include(x => x.Culture)
            .Include(x => x.TypingLevel)
            .Where(x => EF.Functions.Like(x.Culture.CultureCode, cultureCode))
            .ToListAsync();
    }
}