using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypingMaster.Database.Stores;

namespace TypingMaster.Database;

public static class Registration
{
    private const string DbName = "LocalDb";

    public static IServiceCollection AddDatabase(this IServiceCollection services, bool serverMode)
    {
        services.AddDbContext<TestDbContext>((sp, options) =>
        {
            if (serverMode)
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                options.UseSqlite(configuration.GetConnectionString(DbName));
            }
            else
            {
                options.UseInMemoryDatabase(DbName);
            }
        });
        
        if (serverMode)
            services.BuildServiceProvider().GetRequiredService<TestDbContext>().Database.MigrateAsync();

        services.AddSingleton<ITestStore, TestStore>();
        return services;
    }
}