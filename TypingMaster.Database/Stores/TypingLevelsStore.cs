using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Stores;

public class TypingLevelsStore : BaseRepository<TypingLevelEntity>, ITypingLevelsStore
{
    private readonly ILogger<TypingLevelsStore> _logger;
    private readonly IServiceProvider _serviceProvider;

    public TypingLevelsStore(ILogger<TypingLevelsStore> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task<TypingLevelEntity> GetByLevelAsync(long level)
    {
        _logger.LogInformation("GetByLevelAsync | Level={level}", level);

        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var entity = await context.TypingLevels
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DifficultyLevel == level);
        return entity;
    }
}