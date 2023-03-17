using NLog;
using NLog.Web;
using TypingMaster.Shared;

namespace TypingMaster;

public static class Program
{
    private const string appGuid = "d805eda3-817f-42f5-8a41-ede8847b1ec6";

    public static async Task Main(string[] args)
    {
        
        AppDomain.CurrentDomain.ProcessExit += OnProcessExit; 
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

        LogManager.GetCurrentClassLogger().Info("Start program: {program}", Constants.AppFriendlyName);
        LogManager.GetCurrentClassLogger().Info("Version: {version}", Constants.Version);

        if (!new SingleInstanceProtector(appGuid).CheckOneInstanceRunning())
        {
            LogManager.GetCurrentClassLogger().Error("{AppName} already running", Constants.AppFriendlyName);
            return;
        }
        
        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();
        host.Dispose();
    }

   

    private static void OnUnhandledException(object? sender, UnhandledExceptionEventArgs e)
    {
        LogManager.GetCurrentClassLogger().Fatal(e.ExceptionObject);
    }

    private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        foreach (var error in e.Exception.Flatten().InnerExceptions)
            LogManager.GetCurrentClassLogger().Fatal(error);
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseNLog(NLogAspNetCoreOptions.Default)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                webBuilder.UseKestrel(options => options.Limits.MaxRequestBodySize = null);
#if DEBUG
                webBuilder.UseWebRoot("wwwroot");
                webBuilder.UseStaticWebAssets();
#endif
            });
    }
}