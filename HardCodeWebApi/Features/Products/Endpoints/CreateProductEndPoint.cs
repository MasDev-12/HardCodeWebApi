using FastEndpoints;
using HardCodeWebApi.ApplicationDataBase;
using HardCodeWebApi.Features.Products.Domain;
using HardCodeWebApi.Features.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace HardCodeWebApi.Features.Products.Endpoints;

public class CreateProductEndPoint : Endpoint<CreateProductRequest, CreateProductResponse>
{
    private readonly ILogger<CreateProductEndPoint> _logger;
    private readonly ApplicationDbContext _applicationDbContext;
    public CreateProductEndPoint(ILogger<CreateProductEndPoint> logger, ApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }
    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("create_product");
        AllowAnonymous();
    }
    public override async Task HandleAsync(CreateProductRequest request, CancellationToken cancellationToken)
    {
        {
            _logger.LogInformation($"Request CreateProductEndPoint started");
            var productExists = await _applicationDbContext.Products.Include(p => p.ProductFields).SingleOrDefaultAsync(p => p.Name == request.Name);
            if (productExists is not null)
            {
                //TODO problemDetails
                //TODO FluentValidation
                throw new Exception("This product already in DB");
            }
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId
            };
            await _applicationDbContext.Products.AddAsync(product, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            var productFields = request.ProductField.Select(field => new ProductField
            {
                AdditionalFieldId = field.AdditionalFieldId,
                Value = field.Value,
                ProductId = product.Id
            }).ToList();
            await _applicationDbContext.ProductFields.AddRangeAsync(productFields, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var response = new CreateProductResponse
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
}

public class CreateProductRequest
{
    public CreateProductRequest()
    {
        ProductField = new HashSet<ProductFieldDTO>();
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public ICollection<ProductFieldDTO> ProductField { get; set; }
}

public class CreateProductResponse
{
    public ProductDTO ProductDTO { get; set; }
}
