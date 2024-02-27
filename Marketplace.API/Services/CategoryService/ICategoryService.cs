using Marketplace.Dto;

namespace Marketplace.Services.CategoryService;

public interface ICategoryService
{
    public Task<CategoryDto?> Show(Guid categoryId);
    public Task<CategoryDto> Create(CategoryCreateDto data);
    public Task<PaginatedResponseDto<CategoryDto>> GetAll(CategoryFilterDto query);
}
