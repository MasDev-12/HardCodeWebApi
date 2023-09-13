namespace HardCodeWebApi.Features.Products.Models;

public class ProductDTO
{
    public ProductDTO()
    {
        ProductFields = new HashSet<ProductFieldDTO>();
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public ICollection<ProductFieldDTO> ProductFields { get; set; }
}
