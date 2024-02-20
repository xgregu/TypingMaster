namespace TypingMaster.Database.DefaultData;

public record Translation(string Key, string CultureCode, string Translate);

public class TranslationDataProvider
{
    public IEnumerable<Translation> Translations { get; } = GetTypingLevels();

    private static IEnumerable<Translation> GetTypingLevels() =>
        new List<Translation>
        {
            new("SelectLanguage", CultureConstants.Polish, "Wybierz język"),
            new("SelectLanguage", CultureConstants.English, "Select language"),
            new("SelectLanguage", CultureConstants.German, "Sprache auswählen"),
            new("SelectLanguage", CultureConstants.Spanish, "Seleccionar idioma"),
            new("SelectLanguage", CultureConstants.French, "Choisir la langue"),
            new("SelectLanguage", CultureConstants.Chinese, "选择语言")
        };
}