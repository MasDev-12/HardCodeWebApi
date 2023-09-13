namespace HardCodeWebApi.Features.Categories.Domain;

public class Category
{
    public Category()
    {
        AdditionalFields = new HashSet<AdditionalField>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<AdditionalField> AdditionalFields { get; set; }
}
