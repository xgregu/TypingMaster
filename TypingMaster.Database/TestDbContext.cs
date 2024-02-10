using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Database.DefaultData;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Database;

public class TestDbContext : DbContext
{
    private readonly ILoggerFactory _loggerFactory;

    public TestDbContext(DbContextOptions options, ILoggerFactory loggerFactory) : base(options)
    {
        _loggerFactory = loggerFactory;
    }

    public DbSet<TypingTestEntity> TypingTests { get; set; }
    public DbSet<TypingLevelEntity> TypingLevels { get; set; }
    public DbSet<TypingTextEntity> TypingTexts { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(_loggerFactory);

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        
        
        modelBuilder.Entity<TypingLevelEntity>().HasData(DefaultDataProvider.GetTypingLevels());
        modelBuilder.Entity<TypingTextEntity>().HasData(DefaultDataProvider.GetTypingTexts());
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var dateNow = DateTimeOffset.Now;

        foreach (var entityEntry in ChangeTracker.Entries()
                     .Where(e => e is {Entity: BaseEntity, State: EntityState.Added or EntityState.Modified}))
        {
            if (entityEntry.State == EntityState.Added)
                ((BaseEntity) entityEntry.Entity).CreatedDate = dateNow;

            ((BaseEntity) entityEntry.Entity).LastChangeDate = dateNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        var dateNow = DateTimeOffset.Now;

        foreach (var entityEntry in ChangeTracker.Entries()
                     .Where(e => e is {Entity: BaseEntity, State: EntityState.Added or EntityState.Modified}))
        {
            if (entityEntry.State == EntityState.Added)
                ((BaseEntity) entityEntry.Entity).CreatedDate = dateNow;

            ((BaseEntity) entityEntry.Entity).LastChangeDate = dateNow;
        }

        return base.SaveChanges();
    }
}