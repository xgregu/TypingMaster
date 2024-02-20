using TypingMaster.Application.Interfaces;
using TypingMaster.Database.DefaultData;
using TypingMaster.Database.DefaultData.Interface;
using TypingMaster.Domain;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Initializers;

public class TypingLevelStoreInitializer(ITypingLevelsStore typingLevelStore, TypingLevelsDataProvider typingLevelData) : IInitializable
{
    public uint Priority => 3;
    public Task Initialize() => typingLevelStore.AddRangeAsync(typingLevelData.TypingLevels.ToArray());
}