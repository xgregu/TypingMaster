using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Stores;

public class TranslationStore(ILogger<TranslationStore> logger, IServiceProvider serviceProvider)
    : BaseRepository<TranslationEntity>(logger, serviceProvider), ITranslationStore;