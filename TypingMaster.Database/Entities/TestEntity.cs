using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypingMaster.Domain;

namespace TypingMaster.Database.Entities;

public class TestEntity
{
    public int Id { get; set; }
    public Guid TestId { get; set; }
    public TypingTestType TestType { get; set; }
    public string TextToRewritten { get; set; }
    public string ExecutorName { get; set; }
    public DateTime TestDate { get; set; }
    public int InorrectClicks { get; set; }
    public int TotalClicks { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
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
        builder.Property(x => x.InorrectClicks).IsRequired();
        builder.Property(x => x.TotalClicks).IsRequired();
        builder.Property(x => x.StartTime).IsRequired();
    }
}