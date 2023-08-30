using marketplace_api.Data;
using marketplace_api.Dto;
using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _dataContext;

    public ProductService(DataContext dataContext)
    {
        _dataContext = dataContext;
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
        var product = await _dataContext
            .Products
            .Include(product => product.Category)
            .Include(product => product.ProductStatus)
            .Where(t => t.ProductId == productId)
            .FirstOrDefaultAsync();

        if (product is null)
        {
            return null;
        }

        return GetProductDto(product);
    }

    private ProductDto GetProductDto(Product product)
    {
        return new ProductDto()
        {
            Id = product.Id,
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DiscountedPrice = product.DiscountedPrice,
            Category = BuildProductCategoryDto(product.Category),
            ProductStatus = BuildProductStatusDto(product.ProductStatus)
        };
    }

    private ProductStatusDto BuildProductStatusDto(ProductStatus productStatus)
    {
        return new ProductStatusDto()
        {
            Id = productStatus.Id,
            ProductStatusId = productStatus.ProductStatusId,
            Name = productStatus.Name,
        };
    }
    private CategoryDto BuildProductCategoryDto(Category category)
    {
        return new CategoryDto()
        {
            Id = category.Id,
            CategoryId = category.CategoryId,
            Description = category.Description
        };
    }
}