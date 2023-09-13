using HardCodeWebApi.Features.Categories.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.ApplicationDataBase.ModelBuilders.Categories;

public class CategoryModelBuilder : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> CategoryEntityBuilder)
    {
        CategoryEntityBuilder.ToTable("Categories");
        CategoryEntityBuilder.HasKey(category => category.Id);
        CategoryEntityBuilder.Property(category => category.Id).HasColumnName("Id").IsRequired().UseIdentityAlwaysColumn();
        CategoryEntityBuilder.Property(category => category.Name).HasColumnName("Name").IsRequired();
        CategoryEntityBuilder.HasMany(category => category.AdditionalFields)
                            .WithOne(additionalFields => additionalFields.Category)
                            .HasForeignKey(categoryFields => categoryFields.CategoryId)
                            .OnDelete(DeleteBehavior.Cascade);
    }
}
