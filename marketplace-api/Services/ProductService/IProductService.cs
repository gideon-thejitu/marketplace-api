using marketplace_api.Dto;
using marketplace_api.Models;

namespace marketplace_api.Services.ProductService;

public interface IProductService
{
    public Task<ProductDto> Create(ProductCreateDto data);
    public Task<ProductDto?> Show(Guid productId);
    public Task<ProductDto?> Update(Guid productId, ProductDto data);
    public Task<PaginatedResponseDto<ProductDto>> GetAll(ProductFilterDto query);
}
