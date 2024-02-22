using System.Globalization;

namespace TypingMaster.UI.Localizations.Extensions;

public static class StringExtensions
{
    public static string CapitalizeFirstLetter(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
    }
}