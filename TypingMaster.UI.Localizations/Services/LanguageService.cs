using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;

namespace TypingMaster.UI.Localizations.Services;

public class LanguageService(ILogger<LanguageService> logger, NavigationManager navigationManager,
    IHttpContextAccessor httpContextAccessor)
{
    public Task<bool> SetLanguage(string languageCountryCode)
    {
        if (string.IsNullOrWhiteSpace(languageCountryCode))
            return Task.FromResult(true);

        var currentCulture = GetCurrentCulture().Name;
        logger.LogInformation("SetLanguage | Current: {Current}. Target: {Target}", currentCulture,
            languageCountryCode);

        var targetCultureInfo = new CultureInfo(languageCountryCode);

        if (currentCulture.Contains(languageCountryCode)
            && CultureInfo.CurrentCulture.Name == targetCultureInfo.Name
            && CultureInfo.CurrentUICulture.Name == targetCultureInfo.Name
            && Thread.CurrentThread.CurrentCulture.Name == targetCultureInfo.Name
            && Thread.CurrentThread.CurrentUICulture.Name == targetCultureInfo.Name)
            return Task.FromResult(true);

        CultureInfo.CurrentCulture = targetCultureInfo;
        CultureInfo.CurrentUICulture = targetCultureInfo;
        Thread.CurrentThread.CurrentCulture = targetCultureInfo;
        Thread.CurrentThread.CurrentUICulture = targetCultureInfo;
        CultureInfo.DefaultThreadCurrentCulture = targetCultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = targetCultureInfo;

        var uri = new Uri(navigationManager.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
        var query = $"?culture={Uri.EscapeDataString(languageCountryCode)}&redirectUri={Uri.EscapeDataString(uri)}";
        var newUri = "Culture/SetCulture" + query;
        logger.LogInformation("SetLanguage | NavigateTo {Uri}", newUri);
        navigationManager.NavigateTo(newUri, true);
        return Task.FromResult(false);
    }

    public CultureInfo GetCurrentCulture()
    {
        var context = httpContextAccessor.HttpContext;
        var cookies = context?.Request.Cookies;
        var cultureCookie = cookies?[CookieRequestCultureProvider.DefaultCookieName];

        if (string.IsNullOrWhiteSpace(cultureCookie))
            return CultureConstants.DefaultCulture;

        var cultures = CookieRequestCultureProvider.ParseCookieValue(cultureCookie);
        var cultureCode = cultures?.Cultures.FirstOrDefault().Value;

        if (string.IsNullOrWhiteSpace(cultureCode))
            return CultureConstants.DefaultCulture;

        try
        {
            return new CultureInfo(cultureCode);
        }
        catch (CultureNotFoundException)
        {
            return CultureConstants.DefaultCulture;
        }
    }
}