using Microsoft.Extensions.DependencyInjection;
using TypingMaster.UI.Localizations.Services;

namespace TypingMaster.UI.Localizations;

public static class Registration
{
    public static IServiceCollection AddTypingMasterLocalizations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddLocalization();
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<LanguageService>();

        return serviceCollection;
    }
}