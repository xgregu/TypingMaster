using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain;

namespace TypingMaster.Database;

public class DatabaseInitializer : IInitializable
{
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly TestDbContext _context;

    public DatabaseInitializer(ILogger<DatabaseInitializer> logger, TestDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public uint Priority => 1;
    public async Task Initialize()
    {
        _logger.LogInformation("Initialize");
        await MigrateDatabase();
    }

    private async Task MigrateDatabase()
    {
        _logger.LogInformation("MigrateDatabase");
        await _context.Database.MigrateAsync();
    }
}