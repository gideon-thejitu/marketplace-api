using marketplace_api.Dto;

namespace marketplace_api.Services.ProductService;

public interface IProductService
{
    public Task<ProductDto> Create(ProductCreateDto data);
    public Task<ProductDto?> Show(Guid productId);
    public Task<PaginatedResponseDto<ProductDto>> GetAll(ProductFilterDto query);
}
