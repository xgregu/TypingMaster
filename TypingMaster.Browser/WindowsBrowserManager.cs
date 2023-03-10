using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace TypingMaster.Browser;

public class WindowsBrowserManager : IBrowserManager
{
    private readonly ILogger<WindowsBrowserManager> _logger;

    public WindowsBrowserManager(ILogger<WindowsBrowserManager> logger)
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
                FileName = url
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(StartBrowser));
        }

        return Task.CompletedTask;
    }
}