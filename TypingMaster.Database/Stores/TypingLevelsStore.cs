using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Stores;

public class TypingLevelsStore(ILogger<TypingLevelsStore> logger, IServiceProvider serviceProvider)
    : BaseRepository<TypingLevelEntity>(logger, serviceProvider), ITypingLevelsStore;