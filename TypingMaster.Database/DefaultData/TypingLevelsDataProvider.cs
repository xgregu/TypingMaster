using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.DefaultData;

public record TypingLevelName(uint DifficultyLevel, string CultureCode, string Translate);

public class TypingLevelsDataProvider
{
    public IEnumerable<TypingLevelEntity> TypingLevels { get; } = GetTypingLevels();
    public IEnumerable<TypingLevelName> TypingLevelNames { get; } = GetTypingLevelsNames();

    private static IEnumerable<TypingLevelEntity> GetTypingLevels()
    {
        return new List<TypingLevelEntity>
        {
            new() {DifficultyLevel = 1, DifficultyCoefficient = 0.6},
            new() {DifficultyLevel = 2, DifficultyCoefficient = 0.8},
            new() {DifficultyLevel = 3, DifficultyCoefficient = 1.0},
            new() {DifficultyLevel = 4, DifficultyCoefficient = 1.2},
            new() {DifficultyLevel = 5, DifficultyCoefficient = 1.4}
        };
    }

    private static IEnumerable<TypingLevelName> GetTypingLevelsNames()
    {
        return new List<TypingLevelName>
        {
            new(1, CultureConstants.Polish, "Minimalistyczny"),
            new(2, CultureConstants.Polish, "Krótki"),
            new(3, CultureConstants.Polish, "Standardowy"),
            new(4, CultureConstants.Polish, "Długi"),
            new(5, CultureConstants.Polish, "Bardzo długi"),

            new(1, CultureConstants.English, "Minimalist"),
            new(2, CultureConstants.English, "Short"),
            new(3, CultureConstants.English, "Standard"),
            new(4, CultureConstants.English, "Long"),
            new(5, CultureConstants.English, "Very Long"),

            new(1, CultureConstants.German, "Minimalistisch"),
            new(2, CultureConstants.German, "Kurz"),
            new(3, CultureConstants.German, "Standard"),
            new(4, CultureConstants.German, "Lang"),
            new(5, CultureConstants.German, "Sehr Lang"),

            new(1, CultureConstants.French, "Minimaliste"),
            new(2, CultureConstants.French, "Court"),
            new(3, CultureConstants.French, "Standard"),
            new(4, CultureConstants.French, "Long"),
            new(5, CultureConstants.French, "Très Long"),

            new(1, CultureConstants.Spanish, "Minimalista"),
            new(2, CultureConstants.Spanish, "Corto"),
            new(3, CultureConstants.Spanish, "Estándar"),
            new(4, CultureConstants.Spanish, "Largo"),
            new(5, CultureConstants.Spanish, "Muy Largo"),

            new(1, CultureConstants.Chinese, "简约"),
            new(2, CultureConstants.Chinese, "短"),
            new(3, CultureConstants.Chinese, "标准"),
            new(4, CultureConstants.Chinese, "长"),
            new(5, CultureConstants.Chinese, "非常长")
        };
    }
}