using Microsoft.AspNetCore.Builder;
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
        serviceCollection.AddSingleton<ICultureContext, CultureContext>();

        return serviceCollection;
    }

    public static IApplicationBuilder ConfigureLocalizations(this IApplicationBuilder app)
    {
        var cultureContext = app.ApplicationServices.GetRequiredService<ICultureContext>();
        var requestLocalizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(cultureContext.DefaultCulture.Name)
            .AddSupportedCultures(cultureContext.SupportedCultures.Select(x => x.Name).ToArray())
            .AddSupportedUICultures(cultureContext.SupportedCultures.Select(x => x.Name).ToArray());

        app.UseRequestLocalization(requestLocalizationOptions);
        return app;
    }
}