using System.Globalization;

namespace TypingMaster.UI.Localizations;

public static class CultureConstants
{
    public static readonly CultureInfo DefaultCulture = new(LanguageCountryCode.Polish);
    public static class LanguageCountryCode
    {
        public const string Polish = "pl-PL";
        public const string English = "en-US";
        public const string German = "de-DE";
        public const string Spanish = "es-ES";
        public const string French = "fr-FR";
        public const string Chinese = "zh-CN";
    }
}