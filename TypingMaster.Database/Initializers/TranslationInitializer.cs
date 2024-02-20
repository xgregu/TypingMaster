using TypingMaster.Application.Interfaces;
using TypingMaster.Database.DefaultData;
using TypingMaster.Domain;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Initializers;

public class TranslationInitializer(ITranslationStore translationStore,
        ITranslationInLanguageStore translationInLanguageStore, ICulturesStore culturesStore, TranslationDataProvider translationData)
    : IInitializable
{

    public uint Priority => 5;
    public async Task Initialize()
    {
        var cultures = await culturesStore.GetAllAsync();

        var groupedTranslations = translationData.Translations
            .GroupBy(t => t.Key)
            .Select(group => new
            {
                group.Key,
                Translations = group.ToList()
            });
        
        foreach (var group in groupedTranslations)
        {

            var translationEntity = new TranslationEntity
            {
                Key = group.Key
            };
            
            await translationStore.AddAsync(translationEntity);

            foreach (var translation in group.Translations)
            {
                var culture = cultures.First(x => x.CultureCode == translation.CultureCode);
                var translationInLanguageEntity = new TranslationInLanguageEntity
                {
                    Translation = translation.Translate,
                    CultureId = culture.Id,
                    TranslationEntityId = translationEntity.Id,
                };
                
                await translationInLanguageStore.AddAsync(translationInLanguageEntity);
            }
        }
    }
}
