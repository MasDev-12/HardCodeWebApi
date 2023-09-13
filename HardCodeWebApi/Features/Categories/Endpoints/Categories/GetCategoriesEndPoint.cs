using FastEndpoints;
using HardCodeWebApi.ApplicationDataBase;
using HardCodeWebApi.Features.Categories.Models;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.Features.Categories.Endpoints.Categories;

public class GetCategoriesEndPoint: Endpoint<GetCategoriesRequest, GetCategoriesResponse>
{
    private readonly ILogger<DeleteCategoryEndpoint> _logger;
    private readonly ApplicationDbContext _applicationDbContext;
    public GetCategoriesEndPoint(ILogger<DeleteCategoryEndpoint> logger, ApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("get_categories");
        AllowAnonymous();
    }
    public override async Task HandleAsync(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Request GetCategoriesEndPoint started");
        var categoriesWithFields = await _applicationDbContext.Categories.Include(c => c.AdditionalFields).ToListAsync();
        if (categoriesWithFields.Count == 0)
        {
            //TODO add fluentValidation, nuget FluenValidation.AspNet core
            throw new Exception("Categories not found");
        }
        var response = new GetCategoriesResponse
        {
            CategoriesDTO = categoriesWithFields.Select(c => new CategoryDTO
            {
                 Id = c.Id,
                  Name = c.Name,
                   AdditionalFields = c.AdditionalFields.Select(af => new AdditionalFieldDTO 
                   {
                         Name  = af.Name,
                   }).ToList()
            }).ToList()
        };
        await SendAsync(response, cancellation: cancellationToken);
    }
}
public class GetCategoriesRequest
{
}

public class GetCategoriesResponse
{
    public ICollection<CategoryDTO> CategoriesDTO { get; set; }
}