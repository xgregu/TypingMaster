using TypingMaster.Database.DefaultData;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Initializers;

public class TypingLevelStoreInitializer(ICulturesStore culturesStore, ITypingLevelsStore typingLevelStore,
    ITypingLevelNamesStore typingLevelNamesStore, TypingLevelsDataProvider typingLevelData) : IInitializable
{
    public uint Priority => 3;

    public async Task Initialize()
    {
        await typingLevelStore.AddRangeAsync(typingLevelData.TypingLevels.ToArray());

        var cultures = await culturesStore.GetAllAsync();
        var typingLevels = await typingLevelStore.GetAllAsync();

        foreach (var typingLevelName in typingLevelData.TypingLevelNames)
        {
            var culture = cultures.First(x => x.CultureCode == typingLevelName.CultureCode);
            var typingLevel = typingLevels.First(x => x.DifficultyLevel == typingLevelName.DifficultyLevel);
            var typingLevelNameEntity = new TypingLevelNameEntity
            {
                Name = typingLevelName.Translate,
                CultureId = culture.Id,
                TypingLevelId = typingLevel.Id
            };

            await typingLevelNamesStore.AddAsync(typingLevelNameEntity);
        }
    }
}