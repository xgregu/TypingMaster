using NLog;
using NLog.Web;

namespace TypingMaster;

public class Program
{
    public static void Main(string[] args)
    {
        AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

        LogManager.GetCurrentClassLogger().Info("Start program: {program}", Constants.AppFriendlyName);
        LogManager.GetCurrentClassLogger().Info("Version: {version}", Constants.Version);
        CreateHostBuilder(args).Build().Run();
    }

    private static void OnUnhandledException(object? sender, UnhandledExceptionEventArgs e)
    {
        LogManager.GetCurrentClassLogger().Fatal(e.ExceptionObject);
        Environment.Exit(1);
    }

    private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        LogManager.GetCurrentClassLogger().Fatal((Exception)e.Exception);
        Environment.Exit(1);
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .UseNLog(NLogAspNetCoreOptions.Default);
    }
}