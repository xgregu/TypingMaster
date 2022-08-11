using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypingMaster.Domain;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Entities;

public class TestEntity
{
    public int Id { get; set; }
    public Guid TestId { get; set; }
    public TypingTestType TestType { get; set; }
    public string Text { get; set; }
    public string ExecutorName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int CorrectClicks { get; set; }
    public int InorrectClicks { get; set; }
}
internal class TestEntityConfiguration : IEntityTypeConfiguration<TestEntity>
{
    public void Configure(EntityTypeBuilder<TestEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TestId).IsRequired();
        builder.Property(x => x.TestType).IsRequired();
        builder.Property(x => x.Text).IsRequired();
        builder.Property(x => x.ExecutorName).IsRequired();
        builder.Property(x => x.StartTime).IsRequired();
        builder.Property(x => x.EndTime).IsRequired();
        builder.Property(x => x.CorrectClicks).IsRequired();
        builder.Property(x => x.InorrectClicks).IsRequired();
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
            Text = model.TextToRewritten,
            ExecutorName = model.ExecutorName,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            CorrectClicks = model.CorrectClicks,
            InorrectClicks = model.InorrectClicks
        };
    }

    public static TestComplete ToModel(this TestEntity entity)
    {
        if (entity is null) return null;

        return new TestComplete
        {
            Id = entity.TestId,
            Text = entity.Text,
            ExecutorName = entity.ExecutorName,
            TestType = entity.TestType,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            CorrectClicks = entity.CorrectClicks,
            InorrectClicks = entity.InorrectClicks
        };
    }
}
