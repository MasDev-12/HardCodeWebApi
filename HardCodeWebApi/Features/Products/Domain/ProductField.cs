using HardCodeWebApi.Features.Categories.Domain;

namespace HardCodeWebApi.Features.Products.Domain;

public class ProductField
{
    public int Id { get; set; }
    public int AdditionalFieldId { get; set; }
    public AdditionalField AdditionalField { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string Value { get; set; }
}
