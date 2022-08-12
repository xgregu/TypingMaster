using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using TypingMaster.Domain;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Entities;

public class TestEntity
{
    public int Id { get; set; }
    public Guid TestId { get; set; }
    public TypingTestType TestType { get; set; }
    public string TextToRewritten { get; set; }
    public string ExecutorName { get; set; }
    public int TestLenght { get; set; }
    public int EffectivenessPercentage { get; set; }
    public double ClickPerSecond { get; set; }
    public TimeSpan CompletionTime { get; set; }
    public int Mistakes { get; set; }
    public DateTime TestDate { get; set; }
}

internal class TestEntityConfiguration : IEntityTypeConfiguration<TestEntity>
{
    public void Configure(EntityTypeBuilder<TestEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TestId).IsRequired();
        builder.Property(x => x.TestType).IsRequired();
        builder.Property(x => x.TextToRewritten).IsRequired();
        builder.Property(x => x.ExecutorName).IsRequired();
        builder.Property(x => x.TestLenght).IsRequired();
        builder.Property(x => x.EffectivenessPercentage).IsRequired();
        builder.Property(x => x.ClickPerSecond).IsRequired();
        builder.Property(x => x.CompletionTime).IsRequired();
        builder.Property(x => x.Mistakes).IsRequired();
        builder.Property(x => x.TestDate).IsRequired();
    }
}

internal static class TestEntityExtensions
{
    public static TestEntity ToEntity(this Test model)
    {
        if (model is null)
            return null;

        return new TestEntity
        {
            TestId = model.Id,
            TestType = model.TestType,
            TextToRewritten = model.TextToRewritten,
            ExecutorName = model.ExecutorName,
            TestLenght = model.Statistic.TestLenght,
            EffectivenessPercentage = model.Statistic.EffectivenessPercentage,
            ClickPerSecond = model.Statistic.ClickPerSecond,
            CompletionTime = model.Statistic.CompletionTime,
            Mistakes = model.Statistic.Mistakes,
            TestDate = model.TestDate,
        };
    }

    public static Test ToModel(this TestEntity entity)
    {
        if (entity is null)
            return null;

        return new Test
        {
            Id = entity.TestId,
            TestType = entity.TestType,
            TextToRewritten = entity.TextToRewritten,
            ExecutorName = entity.ExecutorName,
            Statistic = new TestStatistic(entity.TestLenght, entity.EffectivenessPercentage,
                entity.ClickPerSecond, entity.CompletionTime, entity.Mistakes),
            TestDate = entity.TestDate
        };
    }
}