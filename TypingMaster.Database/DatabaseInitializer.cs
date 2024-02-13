using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain;

namespace TypingMaster.Database;

public class DatabaseInitializer(ILogger<DatabaseInitializer> logger, TestDbContext context) : IInitializable
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
        await context.Database.MigrateAsync();
    }
}