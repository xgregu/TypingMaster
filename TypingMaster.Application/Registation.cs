using Microsoft.Extensions.DependencyInjection;

namespace TypingMaster.Application;

public static class Registration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddHostedService<Initializer>();
        return services;
    }
}