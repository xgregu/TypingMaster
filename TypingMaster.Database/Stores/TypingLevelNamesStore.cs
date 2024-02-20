using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Stores;

public class TypingLevelNamesStore(ILogger<TypingLevelNamesStore> logger, IServiceProvider serviceProvider)
    : BaseRepository<TypingLevelNameEntity>(logger, serviceProvider), ITypingLevelNamesStore
{
    public async Task<IReadOnlyList<TypingLevelNameEntity>> GetAllAsync(string cultureCode)
    {
        logger.LogInformation("GetAllAsync");
        await using var context = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        return await context.TypingLevelName
            .AsNoTracking()
            .Include(x => x.Culture)
            .Include(x => x.TypingLevel)
            .Where(x => x.Culture.CultureCode.Equals(cultureCode))
            .ToListAsync();
    }
}