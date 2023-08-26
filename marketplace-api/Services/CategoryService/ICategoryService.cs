using marketplace_api.Dto;

namespace marketplace_api.Services.CategoryService;

public interface ICategoryService
{
    public Task<CategoryDto?> Show(Guid categoryId);
}
