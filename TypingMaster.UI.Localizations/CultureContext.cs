using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using TypingMaster.UI.Localizations.Extensions;
using TypingMaster.UI.Localizations.Translations;

namespace TypingMaster.UI.Localizations;

public class CultureContext : ICultureContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CultureContext(ILogger<CultureContext> logger, IStringLocalizer<App> stringLocalizer,
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        SupportedCultures = stringLocalizer.GetAvailableCultures().ToList().AsReadOnly();
        logger.LogDebug("Supported languages: {Languages}",
            string.Join(" | ",
                SupportedCultures.Select(x => x.Name + (x.Name == DefaultCulture.Name ? " (Default)" : string.Empty))));
    }

    public IReadOnlyCollection<CultureInfo> SupportedCultures { get; }
    public CultureInfo DefaultCulture => CultureConstants.DefaultCulture;
    public CultureInfo CurrentCulture => GetCurrentCulture();

    private CultureInfo GetCurrentCulture()
    {
        var context = _httpContextAccessor.HttpContext;
        var cookies = context?.Request.Cookies;
        var cultureCookie = cookies?[CookieRequestCultureProvider.DefaultCookieName];

        if (string.IsNullOrWhiteSpace(cultureCookie))
            return DefaultCulture;

        var cultures = CookieRequestCultureProvider.ParseCookieValue(cultureCookie);
        var cultureCode = cultures?.Cultures.FirstOrDefault().Value;

        if (string.IsNullOrWhiteSpace(cultureCode))
            return DefaultCulture;

        try
        {
            return new CultureInfo(cultureCode);
        }
        catch (CultureNotFoundException)
        {
            return DefaultCulture;
        }
    }
}