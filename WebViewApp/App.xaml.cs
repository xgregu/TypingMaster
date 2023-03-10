using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using WebViewApp.Views;

namespace WebViewApp;

public partial class App : Application
{
    private readonly ILogger<App> _logger;
    private readonly string _webViewAppId;
    private readonly IServiceProvider _serviceProvider;


    public App()
    {
        var hostBuilder = Host.CreateDefaultBuilder()
            .UseNLog(NLogAspNetCoreOptions.Default);

        hostBuilder.ConfigureServices(ConfigureServices);
        var host = hostBuilder.Start();

        _logger = host.Services.GetRequiredService<ILogger<App>>();
        _serviceProvider = host.Services.GetRequiredService<IServiceProvider>();

        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging();
        services.AddSingleton<WebViewWindow>();
        services.AddTransient<IBrowserManager, BrowserManager>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _logger.LogInformation("Startup application");
        _serviceProvider.GetRequiredService<IBrowserManager>()
            .StartBrowser(e.Args);
    }


    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        _logger.LogInformation("Exit application");
    }

    private void OnUnhandledException(object? sender, UnhandledExceptionEventArgs e)
    {
        _logger.LogCritical((Exception) e.ExceptionObject, "Fatal exit application");
        Environment.Exit(1);
    }

    private void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        _logger.LogCritical(e.Exception, "Fatal exit application");
        Environment.Exit(1);
    }
}