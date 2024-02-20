using NLog;
using NLog.Web;
using TypingMaster.Shared;

namespace TypingMaster;

public static class Program
{
    private const string AppId = "d805eda3-817f-42f5-8b41-ede8847b1ec6";
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    public static void Main(string[] args)
    {
        AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

        LogManager.GetCurrentClassLogger().Info("Start program: {Program}", Constants.AppFriendlyName);
        LogManager.GetCurrentClassLogger().Info("Version: {Version}", Constants.Version);

        if (!new SingleInstanceProtector(AppId).CheckOneInstanceRunning())
        {
            LogManager.GetCurrentClassLogger().Error("{AppName} already running", Constants.AppFriendlyName);
            return;
        }

        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .UseNLog(NLogAspNetCoreOptions.Default)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseNLog();
            });
    }

    private static void OnUnhandledException(object? sender, UnhandledExceptionEventArgs e)
    {
        Logger.Fatal(e.ExceptionObject);
    }

    private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        foreach (var error in e.Exception.Flatten().InnerExceptions)
            Logger.Fatal(error);
    }

    private static void OnProcessExit(object? sender, EventArgs e)
    {
        Logger.Info("Exit program");
    }
}