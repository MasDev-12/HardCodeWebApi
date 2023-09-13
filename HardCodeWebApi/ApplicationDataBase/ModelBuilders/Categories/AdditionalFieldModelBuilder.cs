using HardCodeWebApi.Features.Categories.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.ApplicationDataBase.ModelBuilders.Categories;

public class AdditionalFieldModelBuilder : IEntityTypeConfiguration<AdditionalField>
{
    public void Configure(EntityTypeBuilder<AdditionalField> AdditionalFieldEntityBuilder)
    {
        AdditionalFieldEntityBuilder.ToTable("AdditionalFields");
        AdditionalFieldEntityBuilder.HasKey(additionalField => additionalField.Id);
        AdditionalFieldEntityBuilder.Property(additionalField => additionalField.Id).HasColumnName("Id").IsRequired().UseIdentityAlwaysColumn();
        AdditionalFieldEntityBuilder.Property(additionalField => additionalField.Name).HasColumnName("Name").IsRequired();
        AdditionalFieldEntityBuilder.HasOne(additionalField => additionalField.Category)
                                    .WithMany(category => category.AdditionalFields)
                                    .HasForeignKey(additionalField => additionalField.CategoryId)
                                    .OnDelete(DeleteBehavior.Cascade);
    }
}
