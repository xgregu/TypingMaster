using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Domain.Entities;

public class TypingLevelNameEntity : BaseEntity
{
    public string Name { get; set; }
    public CultureEntity Culture { get; set; }
    public long CultureId { get; set; }
    public TypingLevelEntity TypingLevel { get; set; }
    public long TypingLevelId { get; set; }
}

public class TypingLevelNameEntityConfiguration : IEntityTypeConfiguration<TypingLevelNameEntity>
{
    public void Configure(EntityTypeBuilder<TypingLevelNameEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder
            .HasOne(x => x.Culture)
            .WithMany(x => x.TypingLevelNames)
            .HasForeignKey(x => x.CultureId)
            .IsRequired();

        builder
            .HasOne(x => x.TypingLevel)
            .WithMany(x => x.TypingLevelNames)
            .HasForeignKey(x => x.TypingLevelId)
            .IsRequired();
    }
}