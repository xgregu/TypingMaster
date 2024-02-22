using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Stores;

public class TypingLevelsStore(ILogger<TypingLevelsStore> logger, IServiceScopeFactory scopeFactory)
    : BaseRepository<TypingLevelEntity>(logger, scopeFactory), ITypingLevelsStore;