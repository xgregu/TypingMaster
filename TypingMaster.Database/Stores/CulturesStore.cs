using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Stores;

public class CulturesStore(ILogger<CulturesStore> logger, IServiceProvider serviceProvider)
    : BaseRepository<CultureEntity>(logger, serviceProvider), ICulturesStore;