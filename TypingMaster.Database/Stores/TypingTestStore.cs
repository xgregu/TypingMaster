using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.Stores;

public class TypingTestStore : BaseRepository<TypingTestEntity>, ITypingTestStore
{
    private readonly ILogger<TypingTestStore> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly ITypingTextsStore _typingTextsStore;

    public TypingTestStore(ILogger<TypingTestStore> logger, IServiceProvider serviceProvider, ITypingTextsStore typingTextsStore) : base(logger, serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _typingTextsStore = typingTextsStore;
    }

    public async Task<TypingTestEntity> AddAsync(TypingTestEntity entity)
    {
        _logger.LogInformation("AddAsync | {@entity}", entity);
        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        await context.AddAsync(entity);
        await context.SaveChangesAsync();

        var savedEntity = await GetByIdAsync(entity.Id);
        return savedEntity;
    }

    public override async Task<IReadOnlyList<TypingTestEntity>> GetAllAsync()
    {
        _logger.LogInformation("GetAllAsync");
        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        return await context.TypingTests
            .AsNoTracking()
            .Include(x => x.Text)
            .ThenInclude(x => x.DifficultyLevel)
            .Include(x => x.Statistics)
            .ToListAsync();
    }
    
    public override async Task<TypingTestEntity> GetByIdAsync(long id)
    {
        _logger.LogInformation("GetByIdAsync | Id={id}", id);

        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var entity = await context.TypingTests
            .AsNoTracking()
            .Include(x => x.Text)
            .ThenInclude(x => x.DifficultyLevel)
            .Include(x => x.Statistics)
            .FirstOrDefaultAsync(x => x.Id == id);
        return entity;
    }

    public async Task<TypingTestEntity> GetLast()
    {
        _logger.LogInformation("GetLast");
        await using var context = _serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var entity = await context.TypingTests
            .AsNoTracking()
            .Include(x => x.Text)
            .ThenInclude(x => x.DifficultyLevel)
            .Include(x => x.Statistics)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        return entity;
    }
    
}