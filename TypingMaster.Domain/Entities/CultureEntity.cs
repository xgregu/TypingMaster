using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Domain.Entities;

public class CultureEntity : BaseEntity
{
    public string CultureCode { get; set; }
    public ICollection<TypingTextEntity> TypingTexts { get; set; } = new List<TypingTextEntity>();
    public ICollection<TypingLevelNameEntity> TypingLevelNames { get; set; } = new List<TypingLevelNameEntity>();
}

public class CultureEntityConfiguration : IEntityTypeConfiguration<CultureEntity>
{
    public void Configure(EntityTypeBuilder<CultureEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasIndex(x => x.CultureCode).IsUnique();
    }
}