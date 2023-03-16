using Microsoft.Extensions.DependencyInjection;

namespace TypingMaster.Domain;

public static class Registration
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddTransient<ITestService, TestService>();
        return services;
    }
}