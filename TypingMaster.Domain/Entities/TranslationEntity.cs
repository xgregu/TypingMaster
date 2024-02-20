using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Domain.Entities;

public class TranslationEntity : BaseEntity
{
    public string Key { get; set; }
    public ICollection<TranslationInLanguageEntity> TranslationsInLanguages { get; set; } = new List<TranslationInLanguageEntity>();
}

public partial class TranslationEntityConfiguration : IEntityTypeConfiguration<TranslationEntity>
{
    public void Configure(EntityTypeBuilder<TranslationEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasIndex(x => x.Key)
            .IsUnique();
    }
}