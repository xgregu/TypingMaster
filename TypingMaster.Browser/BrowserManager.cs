using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using TypingMaster.Browser.Events;

namespace TypingMaster.Browser;

public class WindowsBrowserManager : IBrowserManager
{
    private const string BrowserAppName = "BrowserApp.exe";
    private readonly ILogger<WindowsBrowserManager> _logger;
    private readonly IMediator _mediator;
    private Process? _browserProcess;

    public WindowsBrowserManager(ILogger<WindowsBrowserManager> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task StartBrowser(string url)
    {
        _logger.LogInformation("{StartBrowser} | {Url}", nameof(StartBrowser), url);
        if (File.Exists(BrowserAppName) && OperatingSystem.IsWindows())
            _browserProcess = await StartBrowserApp();

        _browserProcess ??= await StartDefaultBrowser(url);

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

    private async Task<Process?> StartBrowserApp()
    {
        _logger.LogInformation("StartBrowserApp");
        return await InternalStart("BrowserApp.exe");
    }

    private async Task<Process?> StartDefaultBrowser(string url)
    {
        _logger.LogInformation("StartDefaultBrowser");
        return await InternalStart(url);
    }

    private async Task<Process?> InternalStart(string fileName)
    {
        Action? _unsubscribeProcessExitedEvent = null;

        try
        {
            var process = new Process
            {
                StartInfo = {UseShellExecute = true, FileName = fileName},
                EnableRaisingEvents = true
            };

            process.Exited += ObBrowserAppExited;
            _unsubscribeProcessExitedEvent = () =>
            {
                if (process is not null)
                    process.Exited -= ObBrowserAppExited;
            };

            process.Start();

            return process;
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(InternalStart));
            _unsubscribeProcessExitedEvent.Invoke();
            return null;
        }
    }

    private void ObBrowserAppExited(object? sender, EventArgs e)
    {
        _logger.LogInformation("StartWatcher | Browser app was closed");
        _mediator.Publish(new BrowserAppClosed());
    }
}