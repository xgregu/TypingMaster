using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Application;

public class Initializer(IServiceScopeFactory scopeFactory) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = scopeFactory.CreateAsyncScope();
        var initializes = scope.ServiceProvider.GetServices<IInitializable>();
        foreach (var item in initializes.OrderBy(x => x.Priority))
            await item.Initialize();

        await StopAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}