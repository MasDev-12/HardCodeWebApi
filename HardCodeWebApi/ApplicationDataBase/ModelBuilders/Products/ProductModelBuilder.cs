using HardCodeWebApi.Features.Products.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.ApplicationDataBase.ModelBuilders.Products;

public class ProductModelBuilder : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> ProductEntityBuilder)
    {
        ProductEntityBuilder.ToTable("Products");
        ProductEntityBuilder.HasKey(product => product.Id);
        ProductEntityBuilder.Property(product => product.Id).HasColumnName("Id").IsRequired().UseIdentityAlwaysColumn();
        ProductEntityBuilder.Property(product => product.Name).HasColumnName("Name").IsRequired();
        ProductEntityBuilder.Property(product => product.Description).HasColumnName("Description").IsRequired().HasMaxLength(1000);
        ProductEntityBuilder.Property(product => product.Price).HasColumnName("Price").IsRequired();
    }
}

