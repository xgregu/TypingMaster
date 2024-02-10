using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Stores;

public class TypingTextsStore : BaseRepository<TypingTextEntity>, ITypingTextsStore
{
    private readonly ILogger<TypingTextsStore> _logger;
    private readonly IServiceProvider _serviceProvider;

    public TypingTextsStore(ILogger<TypingTextsStore> logger, IServiceProvider serviceProvider) : base(logger, serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public override async Task<TypingTextEntity> GetByIdAsync(long id)
    {
        _logger.LogInformation("GetByIdAsync | Id={id}", id);

        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var entity = await context.TypingTexts
            .AsNoTracking()
            .Include(x => x.DifficultyLevel)
            .FirstOrDefaultAsync(x => x.Id == id);
        return entity;
    }
}