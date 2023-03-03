using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TypingMaster.Browser;

public static class Registration
{
    public static IServiceCollection AddBrowser(this IServiceCollection services, bool serverMode)
    {
        services.AddTransient<IBrowserManager, BrowserManager>();

        if (!serverMode)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var url = configuration.GetRequiredSection("Urls").Value.Replace("0.0.0.0", "localhost");
            var browserManager = services.BuildServiceProvider().GetRequiredService<IBrowserManager>();
            browserManager.StartBrowser(url);
        }

        return services;
    }
}