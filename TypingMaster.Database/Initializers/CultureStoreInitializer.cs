using TypingMaster.Application.Interfaces;
using TypingMaster.Database.DefaultData;
using TypingMaster.Domain;

namespace TypingMaster.Database.Initializers;

public class CultureStoreInitializer(ICulturesStore culturesStore, CultureDataProvider culturesData) : IInitializable
{
    public uint Priority => 2;

    public Task Initialize()
    {
        return culturesStore.AddRangeAsync(culturesData.Cultures.ToArray());
    }
}