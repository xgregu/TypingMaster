using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.DefaultData;

public class CultureDataProvider
{
    public IEnumerable<CultureEntity> Cultures { get; } = GetCultures();

    private static IEnumerable<CultureEntity> GetCultures()
    {
        return new List<CultureEntity>
        {
            new() {CultureCode = CultureConstants.Polish},
            new() {CultureCode = CultureConstants.English},
            new() {CultureCode = CultureConstants.German},
            new() {CultureCode = CultureConstants.Spanish},
            new() {CultureCode = CultureConstants.French},
            new() {CultureCode = CultureConstants.Chinese}
        };
    }
}