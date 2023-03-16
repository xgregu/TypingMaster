using MediatR.Courier;
using TypingMaster.Browser.Events;

namespace TypingMaster;

public class Stopup : IHostedService
{
    private readonly ILogger<Stopup> _logger;
    private readonly ICourier _courier;
    private readonly IHost _host;

    public Stopup(ILogger<Stopup> logger, ICourier courier, IHost host)
    {
        _logger = logger;
        _courier = courier;
        _host = host;

        _courier.Subscribe<BrowserAppClosed>(OnBrowserAppClosed);
    }

    private async Task OnBrowserAppClosed(BrowserAppClosed _)
    {
        _logger.LogInformation("OnBrowserAppClosed");
        await _host.StopAsync();
    }

    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}