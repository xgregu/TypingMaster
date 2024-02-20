using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Database;

public class TestDbContext(DbContextOptions options, ILoggerFactory loggerFactory) : DbContext(options)
{
    public DbSet<TypingTestEntity> TypingTests { get; set; }
    public DbSet<TypingLevelEntity> TypingLevels { get; set; }
    public DbSet<TypingTextEntity> TypingTexts { get; set; }
    public DbSet<TypingTestStatisticsEntity> TypingTestStatistics { get; set; }
    public DbSet<CultureEntity> Cultures { get; set; }
    public DbSet<TranslationEntity> Translations { get; set; }
    public DbSet<TranslationInLanguageEntity> TranslationInLanguages { get; set; }

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(loggerFactory);

        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
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