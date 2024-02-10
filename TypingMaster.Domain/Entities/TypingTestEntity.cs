using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Domain.Entities;

public class TypingTestEntity : BaseEntity
{
    public string ExecutorName { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public TypingTextEntity Text { get; set; }
    public long TextId { get; set; }
    public TypingTestStatisticsEntity Statistics { get; set; }
    public long StatisticsId { get; set; }
}

public partial class TestEntityConfiguration : IEntityTypeConfiguration<TypingTestEntity>
{
    public void Configure(EntityTypeBuilder<TypingTestEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder
            .HasOne(x => x.Text)
            .WithMany(x => x.Tests)
            .HasForeignKey(x => x.TextId);
        
        builder.Property(x => x.ExecutorName)
            .IsRequired();
        
        builder.Property(x => x.StartTime)
            .IsRequired();
        
        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.HasOne(x => x.Statistics)
            .WithOne(x => x.TypingTest);
    }
}