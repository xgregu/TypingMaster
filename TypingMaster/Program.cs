using NLog;
using NLog.Web;
using TypingMaster.Shared;

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
    }

    private static void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        LogManager.GetCurrentClassLogger().Fatal((Exception)e.Exception);
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