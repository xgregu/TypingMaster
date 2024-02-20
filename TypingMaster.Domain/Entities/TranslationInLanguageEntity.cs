using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypingMaster.Domain.Entities.Common;

namespace TypingMaster.Domain.Entities;

public class TranslationInLanguageEntity : BaseEntity
{
    public string Translation { get; set; }
    
    public CultureEntity Culture { get; set; }
    public long CultureId { get; set; }
    
    public TranslationEntity TranslationEntity { get; set; }
    public long TranslationEntityId { get; set; }
}

public partial class TranslationInLanguageEntityConfiguration : IEntityTypeConfiguration<TranslationInLanguageEntity>
{
    public void Configure(EntityTypeBuilder<TranslationInLanguageEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(x => x.TranslationEntity)
            .WithMany(x => x.TranslationsInLanguages)
            .HasForeignKey(x => x.TranslationEntityId)
            .IsRequired();
        
        builder
            .HasOne(x => x.Culture)
            .WithMany(x => x.TranslationInLanguages)
            .HasForeignKey(x => x.CultureId)
            .IsRequired();
    }
}