using marketplace_api.Data;
using marketplace_api.Dto;
using marketplace_api.Models;
using marketplace_api.Services.Pagination;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _dataContext;
    private readonly IPaginationService _paginationService;

    public ProductService(DataContext dataContext, IPaginationService paginationService)
    {
        _dataContext = dataContext;
        _paginationService = paginationService;
    }
    public async Task<ProductDto> Create(ProductCreateDto data)
    {
        var product = new Product()
        {
            Name = data.Name,
            Description = data.Description,
            CategoryId = data.CategoryId,
            ProductStatusId = data.ProductStatusId,
            Price = data.Price,
            DiscountedPrice = data.DiscountedPrice
        };
            
        _dataContext.Products.Add(product);
        await _dataContext.SaveChangesAsync();

        return GetProductDto(product);
    }

    public async Task<ProductDto?> Show(Guid productId)
    {
        var product = await ProductQueryable().AsNoTracking()
            .Where(t => t.ProductId == productId)
            .Include(product => product.Category)
            .Include(product => product.ProductStatus)
            .FirstOrDefaultAsync();

        if (product is null)
        {
            return null;
        }

        return GetProductDto(product);
    }

    private static ProductDto GetProductDto(Product product)
    {
        
        return new ProductDto()
        {
            Id = product.Id,
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DiscountedPrice = product.DiscountedPrice,
            Category = BuildProductCategoryDto(product?.Category ?? null),
            ProductStatus = BuildProductStatusDto(product?.ProductStatus ?? null)
        };
    }

    private static ProductStatusDto? BuildProductStatusDto(ProductStatus? productStatus)
    {
        if (productStatus is null)
        {
            return null;
        }

        return new ProductStatusDto()
        {
            Id = productStatus.Id,
            ProductStatusId = productStatus.ProductStatusId,
            Name = productStatus.Name,
        };
    }

    public async Task<PaginatedResponseDto<ProductDto>> GetAll(ProductFilterDto query)
    {
        var queryable = ProductQueryableWithDefaultScopes()
            .AsNoTracking();
        var total = await queryable.CountAsync();
        var paginated = await _paginationService
            .Paginate(queryable, query)
            .Include(product => product.ProductStatus)
            .Include(product => product.Category)
            .Select(product => GetProductDto(product)).ToListAsync();

        return new PaginatedResponseDto<ProductDto>()
        {
            Page = query.Page,
            Limit = query.Limit,
            Total = total,
            Results = paginated
        };
    }
    private static CategoryDto? BuildProductCategoryDto(Category? category)
    {
        if (category == null)
        {
            return null;
        }

        return new CategoryDto()
        {
            Id = category.Id,
            Name = category.Name,
            CategoryId = category.CategoryId,
            Description = category.Description
        };
    }

    private IQueryable<Product> ProductQueryableWithDefaultScopes()
    {
        return ProductQueryable().OrderBy(product => product.Id);
    }

    private IQueryable<Product> ProductQueryable()
    {
        return _dataContext.Products;
    }
}