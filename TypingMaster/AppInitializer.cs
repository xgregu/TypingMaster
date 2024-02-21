using System.Text.Json;
using TypingMaster.Domain;
using TypingMaster.Shared.Dtos;

namespace TypingMaster;

public static class AppInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.CreateScope();
        var initializables = serviceScope.ServiceProvider.GetServices<IInitializable>();
        foreach (var item in initializables.OrderBy(x => x.Priority))
            await item.Initialize();
    }
}