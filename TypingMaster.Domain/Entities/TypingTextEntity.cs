using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Domain.Entities;

public class TypingTextEntity : BaseEntity
{
    public string Text { get; set; }
    public CultureEntity Culture { get; set; }
    public long CultureId { get; set; }
    public TypingLevelEntity DifficultyLevel { get; set; }
    public long DifficultyLevelId { get; set; }
    public ICollection<TypingTestEntity> Tests { get; set; } = new List<TypingTestEntity>();
}

public partial class TypingTextEntityConfiguration : IEntityTypeConfiguration<TypingTextEntity>
{
    public void Configure(EntityTypeBuilder<TypingTextEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(x => x.DifficultyLevel)
            .WithMany(x => x.TypingTexts)
            .HasForeignKey(x => x.DifficultyLevelId)
            .IsRequired();

        builder
            .HasOne(x => x.Culture)
            .WithMany(x => x.TypingTexts)
            .HasForeignKey(x => x.CultureId)
            .IsRequired();

        builder.Property(x => x.Text).IsRequired();
    }
}