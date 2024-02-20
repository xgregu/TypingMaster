using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypingMaster.Application.Interfaces;
using TypingMaster.Database.DefaultData;
using TypingMaster.Database.Initializers;
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
        services.AddSingleton<ICulturesStore, CulturesStore>();
        services.AddSingleton<ITypingTextsStore, TypingTextsStore>();
        services.AddSingleton<ITypingLevelNamesStore, TypingLevelNamesStore>();

        services.AddTransient<IInitializable, DatabaseInitializer>();

        if (IsDatabaseExists(services))
            return services;

        services.AddSingleton<CultureDataProvider>();
        services.AddSingleton<TypingTextsDataProvider>();
        services.AddSingleton<TypingLevelsDataProvider>();

        services.AddTransient<IInitializable, CultureStoreInitializer>();
        services.AddTransient<IInitializable, TypingLevelStoreInitializer>();
        services.AddTransient<IInitializable, TypingTextStoreInitializer>();

        return services;
    }

    private static bool IsDatabaseExists(IServiceCollection services)
    {
        using var serviceScope = services.BuildServiceProvider().CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<TestDbContext>();
        return dbContext.Database.CanConnect();
    }
}