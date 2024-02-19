using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TypingMaster.UI.Localizations.Controllers;

[Route("[controller]/[action]")]
public class CultureController(ILogger<CultureController> logger) : Controller
{
    public IActionResult SetCulture(string culture, string redirectUri)
    {
        logger.LogInformation("SetCulture | Culture[{Culture}], RedirectUri[{RedirectUri}]", culture, redirectUri);
        if (!string.IsNullOrWhiteSpace(culture))
        {
            var requestCulture = new RequestCulture(culture);
            logger.LogInformation(
                "HttpContext.Response.Cookies.Append | DefaultCookieName[{DefaultCookieName}], MakeCookieValue[{MakeCookieValue}]",
                CookieRequestCultureProvider.DefaultCookieName, redirectUri);
            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(requestCulture));
        }

        var local = LocalRedirect(redirectUri);
        logger.LogInformation("SetCulture | LocalRedirect[{@Local}]", local);
        return local;
    }
}