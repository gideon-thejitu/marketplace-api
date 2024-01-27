using Marketplace.Domain.Entities;
using Marketplace.Dto;
using Marketplace.Infrastructure.Data;
using Marketplace.Services.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.CategoryService;

public class CategoryService : ICategoryService
{
    private readonly DataContext _dataContext;
    private readonly IPaginationService _paginationService; 

    public CategoryService(DataContext dataContext, IPaginationService paginationService)
    {
        _dataContext = dataContext;
        _paginationService = paginationService;
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

    public async Task<CategoryDto> Create(CategoryCreateDto data)
    {
        var result = await _dataContext.Categories.AddAsync(new Category()
        {
            Name = data.Name,
            Description = data.Description ?? String.Empty
        });

        await _dataContext.SaveChangesAsync();

        return new CategoryDto()
        {
            Id = result.Entity.Id,
            CategoryId = result.Entity.CategoryId,
            Name = result.Entity.Name,
            Description = result.Entity.Description,
        };
    }

    public async Task<PaginatedResponseDto<CategoryDto>> GetAll(CategoryFilterDto query)
    {
        var queryable = CategoryQueryable()
            .AsNoTracking()
            .OrderBy(category => category.Id);;
        var total = await queryable.CountAsync();
        var paginated = await _paginationService.Paginate(queryable, query).Select(category => new CategoryDto()
        {
            Id = category.Id,
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description,
        }).ToListAsync();
        
        return new PaginatedResponseDto<CategoryDto>()
        {
            Page = query.Page,
            Limit = query.Limit,
            Total = total,
            Results = paginated
        };
    }
    
    private IQueryable<Category> CategoryQueryable()
    {
        return _dataContext.Categories;
    }
}