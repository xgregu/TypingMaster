using System;
using System.Threading.Tasks;
using System.Windows;
using BrowserApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace BrowserApp;

public partial class App : Application
{
    private readonly ILogger<App> _logger;
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
        services.AddSingleton<BrowserWindow>();
        services.AddSingleton<ModuleSignalRConnectivity>();
        services.AddTransient<IBrowserManager, BrowserManager>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        _logger.LogInformation("Startup application");

        _serviceProvider.GetRequiredService<ModuleSignalRConnectivity>();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);
        _logger.LogInformation("Exit application");
    }

    private void OnUnhandledException(object? sender, UnhandledExceptionEventArgs e)
    {
        _logger.LogCritical((Exception)e.ExceptionObject, "Fatal exit application");
        Environment.Exit(1);
    }

    private void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        _logger.LogCritical(e.Exception, "Fatal exit application");
        Environment.Exit(1);
    }
}