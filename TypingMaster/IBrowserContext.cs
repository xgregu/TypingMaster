using System.Globalization;
using PSC.Blazor.Components.BrowserDetect;

namespace TypingMaster;

public interface IBrowserContext
{
    public BrowserInfo BrowserInfo { get; }
    public void SetBrowserInfo(BrowserInfo browserInfo);
    public CultureInfo BrowserCulture { get; }
    public void SetBrowserCulture(string languageCode);
}

public class BrowserContext : IBrowserContext
{
    public BrowserInfo BrowserInfo { get; private set; }
    public CultureInfo BrowserCulture { get; private set; }

    public void SetBrowserInfo(BrowserInfo browserInfo)
    {
        BrowserInfo = browserInfo;
    }

    public void SetBrowserCulture(string languageCode)
    {
        BrowserCulture = new CultureInfo(languageCode);
    }
}