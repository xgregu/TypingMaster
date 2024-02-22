using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Initializers;

public class DatabaseInitializer
    (ILogger<DatabaseInitializer> logger, IDbContextFactory<TestDbContext> dbFactory) : IInitializable
{
    public uint Priority => 1;

    public async Task Initialize()
    {
        logger.LogInformation("Initialize");
        await MigrateDatabase();
    }

    private async Task MigrateDatabase()
    {
        logger.LogInformation("MigrateDatabase");
        await using var dbContext = await dbFactory.CreateDbContextAsync();
        await dbContext.Database.MigrateAsync();
    }
}