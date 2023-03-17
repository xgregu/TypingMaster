using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TypingMaster.Shared.Events;

public record BrowserAppClosed : INotification;

public class BrowserAppClosedHandler : INotificationHandler<BrowserAppClosed>
{
    private readonly ILogger<BrowserAppClosedHandler> _logger;
    private readonly IHost _host;
    
    public BrowserAppClosedHandler(ILogger<BrowserAppClosedHandler> logger, IHost host)
    {
        _logger = logger;
        _host = host;
    }

    public async Task Handle(BrowserAppClosed notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("OnBrowserAppClosed");
        await _host.StopAsync(cancellationToken);
    }
}