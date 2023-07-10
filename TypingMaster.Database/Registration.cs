using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypingMaster.Database.Stores;

namespace TypingMaster.Database;

public static class Registration
{
    private const string DbName = "LocalDb";

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<TestDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            options.UseSqlite(configuration.GetConnectionString(DbName));
        });

        services.BuildServiceProvider().GetRequiredService<TestDbContext>().Database.MigrateAsync();

        services.AddSingleton<ITestStore, TestStore>();
        return services;
    }
}