using TypingMaster.Database.DefaultData;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Initializers;

public class TypingTextStoreInitializer
    (ITypingTextsStore typingTextStore, TypingTextsDataProvider typingTextData) : IInitializable
{
    public uint Priority => 4;

    public Task Initialize()
    {
        return typingTextStore.AddRangeAsync(typingTextData.TypingTexts.ToArray());
    }
}