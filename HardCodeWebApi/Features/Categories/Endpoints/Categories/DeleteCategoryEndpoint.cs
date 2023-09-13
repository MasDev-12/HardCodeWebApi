using FastEndpoints;
using HardCodeWebApi.ApplicationDataBase;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.Features.Categories.Endpoints.Categories;

public class DeleteCategoryEndpoint : Endpoint<DeleteCategoryRequest, DeleteCategoryResponse>
{
    private readonly ILogger<DeleteCategoryEndpoint> _logger;
    private readonly ApplicationDbContext _applicationDbContext;
    public DeleteCategoryEndpoint(ILogger<DeleteCategoryEndpoint> logger, ApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("delete_category");
        AllowAnonymous();
    }
    public override async Task HandleAsync(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Request DeleteCategoryEndpoint started");
        var category = await _applicationDbContext.Categories.SingleOrDefaultAsync(c => c.Id == request.Id);
        if (category is null)
        {
            //TODO add fluentValidation, nuget FluenValidation.AspNet core
            throw new Exception("Category not found");
        }
        var response = new DeleteCategoryResponse
        {
            feedback = "Category deleted"
        };
        await SendAsync(response, cancellation: cancellationToken);
    }
}
public class DeleteCategoryRequest
{
    public int Id { get; set; }
}

public class DeleteCategoryResponse
{
    public string feedback { get; set; }
}
