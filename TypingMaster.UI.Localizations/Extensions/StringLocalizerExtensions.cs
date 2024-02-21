using System.Globalization;
using System.Resources;
using Microsoft.Extensions.Localization;

namespace TypingMaster.UI.Localizations.Extensions;

public static class StringLocalizerExtensions
{
    public static IEnumerable<string> GetTranslationsForRecord<T>(this IStringLocalizer<T> localizer, string recordKey)
    {
        var resourceManager = new ResourceManager(typeof(T));
        var translations = new HashSet<string>();

        foreach (var culture in localizer.GetAvailableCultures())
        {
            var translation = resourceManager.GetString(recordKey, culture);
            if (!string.IsNullOrWhiteSpace(translation))
                translations.Add(translation);
        }

        return translations;
    }

    public static IEnumerable<CultureInfo> GetAvailableCultures<T>(this IStringLocalizer<T> _)
    {
        var assembly = typeof(T).Assembly;
        var baseName = typeof(T).FullName;
        var resourceManager = new ResourceManager(baseName, assembly);

        var cultures = new HashSet<CultureInfo>();

        foreach (var cultureInfo in CultureInfo.GetCultures(CultureTypes.AllCultures))
            try
            {
                using var resourceSet = resourceManager.GetResourceSet(cultureInfo, true, false);
                
                if (resourceSet == null)
                    continue;

                if(string.IsNullOrWhiteSpace(cultureInfo.Name))
                    continue;
                
                cultures.Add(cultureInfo);
            }
            catch (CultureNotFoundException)
            {
            }

        return cultures;
    }
}