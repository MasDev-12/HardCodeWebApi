using HardCodeWebApi.Features.Products.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.ApplicationDataBase.ModelBuilders.Products;

public class ProductFieldsBuilder : IEntityTypeConfiguration<ProductField>
{
    public void Configure(EntityTypeBuilder<ProductField> productFieldbuilder)
    {
        productFieldbuilder.ToTable("ProductField");
        productFieldbuilder.HasKey(productField => productField.Id);
        productFieldbuilder.Property(productField => productField.Id).HasColumnName("Id").IsRequired().UseIdentityAlwaysColumn();
        productFieldbuilder.Property(productField => productField.Value).HasColumnName("Value").IsRequired();
        productFieldbuilder.HasOne(productField => productField.AdditionalField)
                           .WithMany()
                           .HasForeignKey(productField => productField.AdditionalFieldId)
                           .OnDelete(DeleteBehavior.Cascade);
        productFieldbuilder.HasOne(productField => productField.Product)
                           .WithMany(product => product.ProductFields)
                           .HasForeignKey(productField => productField.ProductId)
                           .OnDelete(DeleteBehavior.Cascade);
    }
}