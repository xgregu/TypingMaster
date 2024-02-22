using System.Globalization;

namespace TypingMaster.UI.Localizations.Extensions;

public static class CultureExtensions
{
    public static string RegionName(this CultureInfo cultureInfo)
    {
        return cultureInfo.RegionInfo().Name;
    }

    public static string RegionNativeName(this CultureInfo cultureInfo)
    {
        return cultureInfo.RegionInfo().NativeName;
    }

    public static string TwoLetterISORegionName(this CultureInfo cultureInfo)
    {
        return cultureInfo.RegionInfo().TwoLetterISORegionName;
    }

    public static string LanguageName(this CultureInfo cultureInfo)
    {
        return new CultureInfo(cultureInfo.TwoLetterISOLanguageName).NativeName.CapitalizeFirstLetter();
    }

    public static RegionInfo RegionInfo(this CultureInfo cultureInfo)
    {
        return new RegionInfo(cultureInfo.Name);
    }
}