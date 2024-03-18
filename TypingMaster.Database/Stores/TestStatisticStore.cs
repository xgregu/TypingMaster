using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Interfaces;

namespace TypingMaster.Database.Stores;

public class TestStatisticStore(ILogger<TypingTestStatisticsEntity> logger, IDbContextFactory<TestDbContext> dbFactory)
    : BaseRepository<TypingTestStatisticsEntity>(logger, dbFactory), ITestStatisticStore;