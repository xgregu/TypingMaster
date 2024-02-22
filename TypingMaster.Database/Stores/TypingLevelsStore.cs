using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Stores;

public class TypingLevelsStore(ILogger<TypingLevelsStore> logger, IDbContextFactory<TestDbContext> dbFactory)
    : BaseRepository<TypingLevelEntity>(logger, dbFactory), ITypingLevelsStore;