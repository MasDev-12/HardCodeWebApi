using HardCodeWebApi.ApplicationDbContext.ModelBuilders.Categories;
using HardCodeWebApi.ApplicationDbContext.ModelBuilders.Products;
using HardCodeWebApi.Features.Categories.Domain;
using HardCodeWebApi.Features.Products.Domain;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.ApplicationDbContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CategoryModelBuilder());
        modelBuilder.ApplyConfiguration(new AdditionalFieldModelBuilder());
        modelBuilder.ApplyConfiguration(new ProductModelBuilder());
        modelBuilder.ApplyConfiguration(new ProductFieldsBuilder());
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<AdditionalField> AdditionalFields { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductField> ProductFields { get; set; } = null!;
}
