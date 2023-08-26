using System.Data.Entity;
using marketplace_api.Data;
using marketplace_api.Dto;
using marketplace_api.Models;

namespace marketplace_api.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly DataContext _dataContext;

    public CategoryService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<CategoryDto?> Show(Guid categoryId)
    {
        var queryable = CategoryQueryable().AsNoTracking();

        return await queryable.Where(category => category.CategoryId == categoryId).Select(category => new CategoryDto()
        {
            Id = category.Id,
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description
        }).FirstOrDefaultAsync();
    }

    private IQueryable<Category> CategoryQueryable()
    {
        return _dataContext.Categories;
    }
}