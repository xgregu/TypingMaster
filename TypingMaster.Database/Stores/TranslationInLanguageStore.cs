using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Stores;

public class TranslationInLanguageStore(ILogger<TranslationInLanguageStore> logger, IServiceProvider serviceProvider)
    : BaseRepository<TranslationInLanguageEntity>(logger, serviceProvider), ITranslationInLanguageStore;