using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Domain.Entities;

public class TypingLevelEntity : BaseEntity
{
    public string Name { get; set; }
    public uint DifficultyLevel { get; set; }
    public double DifficultyCoefficient { get; set; }
    public ICollection<TypingTextEntity> TypingTexts { get; set; } = new List<TypingTextEntity>();
}

public partial class TypingLevelEntityConfiguration : IEntityTypeConfiguration<TypingLevelEntity>
{
    public void Configure(EntityTypeBuilder<TypingLevelEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        
        builder.HasIndex(x => x.DifficultyLevel)
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired();
    }
}