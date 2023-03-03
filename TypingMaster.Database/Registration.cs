using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TypingMaster.Database.Stores;

namespace TypingMaster.Database;

public static class Registration
{
    private const string DbName = "LocalDb";

    public static IServiceCollection AddDatabase(this IServiceCollection services, bool isPortable)
    {
        services.AddDbContext<TestDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            if (isPortable)
                options.UseInMemoryDatabase(DbName);
            else
                options.UseSqlite(configuration.GetConnectionString(DbName));
        });
        services.AddSingleton<ITestStore, TestStore>();
        if (!isPortable)
            services.BuildServiceProvider().GetRequiredService<TestDbContext>().Database.MigrateAsync();
        return services;
    }
}