using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TypingMaster.Database.Entities;

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