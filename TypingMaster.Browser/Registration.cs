using System.Diagnostics;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TypingMaster.Browser;

public static class Registration
{
    public static IServiceCollection AddBrowser(this IServiceCollection services, bool isPortable)
    {
        services.AddTransient<IBrowserManager, BrowserManager>();

        if (!isPortable)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            var url = configuration.GetRequiredSection("Urls").Value.Replace("0.0.0.0", "localhost");
            var browserManager = services.BuildServiceProvider().GetRequiredService<IBrowserManager>();
            browserManager.StartBrowser(url);
        }

        return services;
    }
}

public interface IBrowserManager
{
    Task StartBrowser(string url);
}

public class BrowserManager : IBrowserManager
{
    private readonly ILogger<BrowserManager> _logger;

    public BrowserManager(ILogger<BrowserManager> logger)
    {
        _logger = logger;
    }

    public Task StartBrowser(string url)
    {
        try
        {           
            _logger.LogInformation("StartBrowser | {Url}", url);
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = url,
            }).Start();
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(StartBrowser));
        }

        return Task.CompletedTask;
    }
}