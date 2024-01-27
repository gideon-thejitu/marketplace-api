using Marketplace.Dto;

namespace Marketplace.Services.ProductService;

public interface IProductService
{
    public Task<ProductDto> Create(ProductCreateDto data);
    public Task<ProductDto?> Show(Guid productId);
    public Task<ProductDto?> Update(Guid productId, ProductDto data);
    public Task<PaginatedResponseDto<ProductDto>> GetAll(ProductFilterDto query);
    public Task<bool> Exists(Guid productId);
    public Task Destroy(Guid productId);
}
