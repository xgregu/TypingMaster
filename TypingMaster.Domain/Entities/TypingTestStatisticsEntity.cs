using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Domain.Entities;

public class TypingTestStatisticsEntity : BaseEntity
{
    public long EffectivenessPercentage { get; set; }
    public double ClickPerMinute { get; set; }
    public long CompletionTimeMilliseconds { get; set; }
    public long TotalClicks { get; set; }
    public long MistakesClicks { get; set; }
    public long OverallRating { get; set; }
    public TypingTestEntity TypingTest { get; set; }
}

public partial class TypingTextEntityConfiguration : IEntityTypeConfiguration<TypingTestStatisticsEntity>
{
    public void Configure(EntityTypeBuilder<TypingTestStatisticsEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.Property(x => x.EffectivenessPercentage)
            .IsRequired();
        builder.Property(x => x.CompletionTimeMilliseconds)
            .IsRequired();
        builder.Property(x => x.MistakesClicks)
            .IsRequired();
        builder.Property(x => x.OverallRating)
            .IsRequired();
        builder.Property(x => x.TotalClicks)
            .IsRequired();
    }
}