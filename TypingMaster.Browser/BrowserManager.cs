using System.Diagnostics;
using MediatR.Courier;
using Microsoft.Extensions.Logging;
using TypingMaster.Browser.Events;

namespace TypingMaster.Browser;

public class WindowsBrowserManager : IBrowserManager
{
    private const string BrowserAppName = "BrowserApp.exe";
    private readonly ICourier _courier;
    private readonly ILogger<WindowsBrowserManager> _logger;
    private Process? _browserProcess;

    public WindowsBrowserManager(ILogger<WindowsBrowserManager> logger, ICourier courier)
    {
        _logger = logger;
        _courier = courier;

        _courier.Subscribe<BrowserAppDisconected>(OnBrowserAppDisconected);
    }

    public async Task StartBrowser(string url)
    {
        _logger.LogInformation("{StartBrowser} | {Url}", nameof(StartBrowser), url);
        if (File.Exists(BrowserAppName) && OperatingSystem.IsWindows())
            _browserProcess = StartBrowserApp();

        _browserProcess ??= StartDefaultBrowser(url);

        if (_browserProcess is null)
            _logger.LogError("Any browser can't started");
    }

    public async Task CloseBrowser()
    {
        if (_browserProcess is null)
            return;

        _logger.LogInformation("CloseBrowser");
        _browserProcess.Close();
    }

    private Process? StartBrowserApp()
    {
        try
        {
            _logger.LogInformation("StartBrowserApp");
            var process = Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = "BrowserApp.exe"
            });
            return process;
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(StartBrowserApp));
            return null;
        }
    }

    private Process? StartDefaultBrowser(string url)
    {
        _logger.LogInformation("StartDefaultBrowser");

        try
        {
            return Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = url
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(StartDefaultBrowser));
            return null;
        }
    }

    private void OnBrowserAppDisconected(BrowserAppDisconected arg)
    {
        _logger.LogInformation(nameof(OnBrowserAppDisconected));
    }
}