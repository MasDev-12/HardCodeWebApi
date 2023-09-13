namespace HardCodeWebApi.Features.Categories.Models;

public class CategoryDTO
{
    public CategoryDTO()
    {
        AdditionalFields = new HashSet<AdditionalFieldDTO>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<AdditionalFieldDTO> AdditionalFields { get; set; }
}

