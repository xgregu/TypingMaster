using TypingMaster.Application.Interfaces;
using TypingMaster.Database.DefaultData;
using TypingMaster.Database.DefaultData.Interface;
using TypingMaster.Domain;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Initializers;

public class TypingTextStoreInitializer(ITypingTextsStore typingTextStore, TypingTextsDataProvider typingTextData) : IInitializable
{
    public uint Priority => 4;
    public Task Initialize() => typingTextStore.AddRangeAsync(typingTextData.TypingTexts.ToArray());
}