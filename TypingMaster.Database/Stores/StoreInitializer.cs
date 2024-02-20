using TypingMaster.Database.DefaultData;
using TypingMaster.Domain;

namespace TypingMaster.Database.Stores;

public class StoreInitializer(TestDbContext context) : IInitializable
{
    public uint Priority => 2;
    public async Task Initialize()
    {
        await SeedCultures(context);
        await SeedTypingLevels(context);
        await SeedTypingTexts(context);
    }

    private static async Task SeedTypingTexts(TestDbContext context)
    {
        if (!context.TypingTexts.Any())
        {
            foreach (var VARIABLE in DefaultDataProvider.GetTypingTexts())
            {
                try
                {
                    await context.TypingTexts.AddAsync(VARIABLE);
                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                }
            }
            
            context.TypingTexts.AddRange(DefaultDataProvider.GetTypingTexts());
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedTypingLevels(TestDbContext context)
    {
        if (!context.TypingLevels.Any())
        {
            context.TypingLevels.AddRange(DefaultDataProvider.GetTypingLevels());
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedCultures(TestDbContext context)
    {
        if (!context.Cultures.Any())
        {
            context.Cultures.AddRange(DefaultDataProvider.GetCultures());
            await context.SaveChangesAsync();
        }
    }
}