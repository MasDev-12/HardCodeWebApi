using FastEndpoints;
using HardCodeWebApi.ApplicationDataBase;
using HardCodeWebApi.Features.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.Features.Products.Endpoints;

public class GetProductByCategoryIdEndpoint : Endpoint<GetProductByCategoryIdRequest, GetProductByCategoryIdResponse>
{
    private readonly ILogger<GetProductByCategoryIdEndpoint> _logger;
    private readonly ApplicationDbContext _applicationDbContext;
    public GetProductByCategoryIdEndpoint(ILogger<GetProductByCategoryIdEndpoint> logger, ApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("get_product_by_category_id");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductByCategoryIdRequest request, CancellationToken cancellationToken)
    {
        var product = await _applicationDbContext.Products.Include(p => p.ProductFields)
                                                          .SingleOrDefaultAsync(p => p.Id == request.CategoryId, cancellationToken);
        if (product is null)
        {
            //TODO FluentValidation and ProblemDetails
            throw new Exception("Product is empty, please enter the product in DB");
        }
        var response = new GetProductByCategoryIdResponse
        {
            ProductDTO = new ProductDTO
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ProductFields = product.ProductFields.Select(field => new ProductFieldDTO
                {
                    AdditionalFieldId = field.AdditionalFieldId,
                    Value = field.Value
                }).ToList()
            }
        };
        await SendAsync(response, cancellation: cancellationToken);
    }
}

public class GetProductByCategoryIdRequest
{
    public int CategoryId { get; set; }
}
public class GetProductByCategoryIdResponse
{
    public ProductDTO ProductDTO { get; set; }
}
