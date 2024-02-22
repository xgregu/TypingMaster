using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Stores;

public class CulturesStore(ILogger<CulturesStore> logger, IDbContextFactory<TestDbContext> dbFactory)
    : BaseRepository<CultureEntity>(logger, dbFactory), ICulturesStore;