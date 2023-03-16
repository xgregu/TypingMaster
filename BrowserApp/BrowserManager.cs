using System;
using System.Threading.Tasks;
using BrowserApp.Views;
using Microsoft.Extensions.Logging;

namespace BrowserApp;

internal class BrowserManager : IBrowserManager
{
    private readonly BrowserWindow _browserWindow;
    private readonly ILogger<BrowserManager> _logger;

    public BrowserManager(ILogger<BrowserManager> logger, BrowserWindow browserWindow)
    {
        _logger = logger;
        _browserWindow = browserWindow;
    }

    public async Task NavigateTo(string url)
    {
        _logger.LogInformation("NavigateTo | {Url}", url);
        Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var source);
        if (source is null)
            _logger.LogError("Invalid url");

        _browserWindow.WebViewSource = source ?? new Uri("about:blank");
    }

    public async Task SetTitle(string title)
    {
        _logger.LogInformation("SetTitle | {Title}", title);
        _browserWindow.WebViewTitle = title;
    }
}