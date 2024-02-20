using Microsoft.Extensions.DependencyInjection;

namespace TypingMaster.Domain;

public static class Registration
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}