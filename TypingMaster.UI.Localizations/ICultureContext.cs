using System.Globalization;

namespace TypingMaster.UI.Localizations;

public interface ICultureContext
{
    IReadOnlyCollection<CultureInfo> SupportedCultures { get; }
    CultureInfo DefaultCulture { get; }
    CultureInfo CurrentCulture { get; }
}