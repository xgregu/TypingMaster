using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TypingMaster.Domain.Entities;
using TypingMaster.Domain.Entities.Common;
using TypingMaster.Shared;

namespace TypingMaster.Database;

public class TestDbContext
    (ILogger<TestDbContext> logger, DbContextOptions options, ILoggerFactory loggerFactory) : DbContext(options)
{
    public DbSet<TypingTestEntity> TypingTests { get; set; }
    public DbSet<TypingLevelEntity> TypingLevels { get; set; }
    public DbSet<TypingTextEntity> TypingTexts { get; set; }
    public DbSet<TypingTestStatisticsEntity> TypingTestStatistics { get; set; }
    public DbSet<CultureEntity> Cultures { get; set; }
    public DbSet<TypingLevelNameEntity> TypingLevelName { get; set; }

    public async Task<Result<bool, string>> HealthCheck(CancellationToken cancellationToken = default!)
    {
        try
        {
            var isConnected = await Database.CanConnectAsync(cancellationToken);
            if (!isConnected)
            {
                logger.LogError("HealthCheck | Connection failed");
                return Result.Failed<bool, string>("Connection failed");
            }

            var integrityResult = await IntegrityCheck(cancellationToken);
            if (integrityResult.IsError)
            {
                logger.LogError("HealthCheck | Is unhealthy - {error}", integrityResult.Error);
                return integrityResult;
            }

            logger.LogInformation("HealthCheck | Is healthy");
            return Result.Ok<bool, string>(true);
        }
        catch (Exception ex)
        {
            logger.LogError("HealthCheck | Is unhealthy - {error}", ex.Message);
            return Result.Failed<bool, string>(ex.Message);
        }
    }

    private async Task<Result<bool, string>> IntegrityCheck(CancellationToken cancellationToken = default!)
    {
        logger.LogInformation("IntegrityCheck");

        const string checkIntegrityQuery = "PRAGMA integrity_check;";
        const string integrityCheckOk = "ok";

        try
        {
            var connection = Database.GetDbConnection();
            await connection.OpenAsync(cancellationToken);
            await using var command = connection.CreateCommand();
            command.CommandText = checkIntegrityQuery;
            var response = await command.ExecuteScalarAsync(cancellationToken) as string;
            logger.LogInformation("IntegrityCheck | Response=[{response}]", response);
            var isIntegrity =
                response?.Trim().Equals(integrityCheckOk, StringComparison.CurrentCultureIgnoreCase) ??
                false;

            if (isIntegrity)
            {
                logger.LogInformation("IntegrityCheck | Success");
                return Result.Ok<bool, string>(true);
            }

            logger.LogError("IntegrityCheck | Failed");
            return Result.Failed<bool, string>("Integration failed");
        }
        catch (Exception e)
        {
            logger.LogError(e, "IntegrityCheck | Error");
            return Result.Failed<bool, string>(e.Message);
        }
    }

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