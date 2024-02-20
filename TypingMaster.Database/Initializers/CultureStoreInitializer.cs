using TypingMaster.Application.Interfaces;
using TypingMaster.Database.DefaultData;
using TypingMaster.Database.DefaultData.Interface;
using TypingMaster.Domain;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Initializers;

public class CultureStoreInitializer(ICulturesStore culturesStore, CultureDataProvider culturesData) : IInitializable
{
    public uint Priority => 2;
    public Task Initialize() => culturesStore.AddRangeAsync(culturesData.Cultures.ToArray());
}