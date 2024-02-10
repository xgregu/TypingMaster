using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypingMaster.Application.Interfaces;
using TypingMaster.Database.Stores;
using TypingMaster.Domain;

namespace TypingMaster.Database;

public static class Registration
{
    private const string DbName = "LocalDb";
    private const string DbContextMigrationHistoryTableName = "__MigrationHistory";

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<TestDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            options.UseSqlite(configuration.GetConnectionString(DbName),
                o => { o.MigrationsHistoryTable(DbContextMigrationHistoryTableName); });
        });


        services.AddSingleton(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddSingleton<ITypingTestStore, TypingTestStore>();
        services.AddSingleton<ITypingLevelsStore, TypingLevelsStore>();
        services.AddSingleton<ITypingTextsStore, TypingTextsStore>();
        
        services.AddTransient<IInitializable, DatabaseInitializer>();
        return services;
    }
}