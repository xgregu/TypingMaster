using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace TypingMaster.UI.Localizations.Services;

public class LanguageService(ILogger<LanguageService> logger, NavigationManager navigationManager,
    ICultureContext cultureContext)
{
    public bool SetLanguage(string languageCountryCode)
    {
        if (string.IsNullOrWhiteSpace(languageCountryCode))
            return true;

        return SetLanguage(new CultureInfo(languageCountryCode));
    }

    public bool SetLanguage(CultureInfo targetCultureInfo)
    {
        logger.LogInformation("SetLanguage | Current: {Current}. Target: {Target}", cultureContext.CurrentCulture.Name,
            targetCultureInfo.Name);

        if (cultureContext.CurrentCulture.Name == targetCultureInfo.Name
            && CultureInfo.CurrentCulture.Name == targetCultureInfo.Name
            && CultureInfo.CurrentUICulture.Name == targetCultureInfo.Name
            && Thread.CurrentThread.CurrentCulture.Name == targetCultureInfo.Name
            && Thread.CurrentThread.CurrentUICulture.Name == targetCultureInfo.Name)
            return true;

        CultureInfo.CurrentCulture = targetCultureInfo;
        CultureInfo.CurrentUICulture = targetCultureInfo;
        Thread.CurrentThread.CurrentCulture = targetCultureInfo;
        Thread.CurrentThread.CurrentUICulture = targetCultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = targetCultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = targetCultureInfo;

        var uri = new Uri(navigationManager.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
        var query = $"?culture={Uri.EscapeDataString(targetCultureInfo.Name)}&redirectUri={Uri.EscapeDataString(uri)}";
        var newUri = "Culture/SetCulture" + query;
        logger.LogInformation("SetLanguage | NavigateTo {Uri}", newUri);
        navigationManager.NavigateTo(newUri, true);
        return false;
    }
}