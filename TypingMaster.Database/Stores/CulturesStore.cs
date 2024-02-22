using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Stores;

public class CulturesStore(ILogger<CulturesStore> logger, IServiceScopeFactory scopeFactory)
    : BaseRepository<CultureEntity>(logger, scopeFactory), ICulturesStore;