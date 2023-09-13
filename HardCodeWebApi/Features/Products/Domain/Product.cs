using HardCodeWebApi.Features.Categories.Domain;

namespace HardCodeWebApi.Features.Products.Domain;

public class Product
{
    public Product()
    {
        ProductFields = new HashSet<ProductField>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<ProductField> ProductFields { get; set; }
}
