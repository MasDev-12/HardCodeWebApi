using FastEndpoints;
using HardCodeWebApi.ApplicationDataBase;
using HardCodeWebApi.Features.Categories.Domain;
using HardCodeWebApi.Features.Categories.Models;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.Features.Categories.Endpoints.Categories;

public class CreateCategoryEndPoint : Endpoint<CreateCategoryRequest, CreateCategoryResponse>
{
    private readonly ILogger<CreateCategoryEndPoint> _logger;
    private readonly ApplicationDbContext _applicationDbContext;

    public CreateCategoryEndPoint(ILogger<CreateCategoryEndPoint> logger, ApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("create_category");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Create category with name {request.Name}");
        var existsCategory = await _applicationDbContext.Categories.SingleOrDefaultAsync(category => category.Name == request.Name, cancellationToken);
        if (existsCategory is not null)
        {
            //TODO add fluentValidation, nuget FluenValidation.AspNet core
            throw new Exception($"{request.Name} Category already in Db");
        }
        var category = new Category()
        {
            Name = request.Name,
        };
        await _applicationDbContext.AddAsync(category, cancellationToken);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        category.AdditionalFields = request.CategoryFields.Select(field => new AdditionalField
        {
            Name = field.Name,
            CategoryId = category.Id
        }).ToList();
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        var response = new CreateCategoryResponse
        {
            CategoryDTO = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                AdditionalFields = category.AdditionalFields.Select(field => new AdditionalFieldDTO
                {
                    Name = field.Name,
                }).ToList()
            }
        };
        await SendAsync(response, cancellation: cancellationToken);
    }
}
public class CreateCategoryRequest
{
    public CreateCategoryRequest()
    {
        CategoryFields = new HashSet<AdditionalFieldDTO>();
    }
    public string Name { get; set; }
    public ICollection<AdditionalFieldDTO> CategoryFields { get; set; }
}

public class CreateCategoryResponse
{
    public CategoryDTO CategoryDTO { get; set; }
}