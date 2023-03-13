using System.Diagnostics;
using MediatR.Courier;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Events;

namespace TypingMaster.Browser;

public class WindowsBrowserManager : IBrowserManager
{
    private readonly ILogger<WindowsBrowserManager> _logger;

    public WindowsBrowserManager(ILogger<WindowsBrowserManager> logger, ICourier courier)
    {
        _logger = logger;
        courier.Subscribe<WebViewAppDisconected>(OnWebViewAppDisconected);
    }

    public Task StartBrowser(string url)
    {
        try
        {
            _logger.LogInformation("{StartBrowser} | {Url}", nameof(StartBrowser), url);
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = "WebViewApp.exe"
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, nameof(StartBrowser));
            Process.Start(new ProcessStartInfo
            {
                UseShellExecute = true,
                FileName = url
            });
        }
        
        return Task.CompletedTask;
    }

    private void OnWebViewAppDisconected(WebViewAppDisconected arg)
    {
        _logger.LogInformation(nameof(OnWebViewAppDisconected));
    }
}