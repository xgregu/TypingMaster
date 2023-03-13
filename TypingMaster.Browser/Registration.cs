using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TypingMaster.Browser;

public static class Registration
{
    public static IServiceCollection AddBrowser(this IServiceCollection services, bool serverMode)
    {
        if (serverMode)
            return services;

        services.AddSingleton<IBrowserManager, WindowsBrowserManager>();

        StartBrowser(services);
        return services;
    }

    private static void StartBrowser(IServiceCollection services)
    {
        var url = services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetRequiredSection("Urls").Value
            .Replace("0.0.0.0", "localhost");
        var browserManager = services.BuildServiceProvider().GetRequiredService<IBrowserManager>();
        browserManager.StartBrowser(url);
    }
}