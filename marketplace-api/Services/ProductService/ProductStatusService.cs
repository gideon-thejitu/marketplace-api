using marketplace_api.Data;
using marketplace_api.Dto;
using marketplace_api.Models;
using marketplace_api.Services.Pagination;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.Services.ProductService;

public class ProductStatusService : IProductStatusService
{
    private readonly DataContext _dataContext;
    private readonly IPaginationService _paginationService;

    public ProductStatusService(DataContext dataContext, IPaginationService paginationService)
    {
        _dataContext = dataContext;
        _paginationService = paginationService;
    }

    public async Task<ProductStatusDto> Create(ProductStatusCreateDto data)
    {
        var result = await _dataContext.ProductStatuses.AddAsync(new ProductStatus()
        {
            Name = data.Name
        });

        await _dataContext.SaveChangesAsync();

        return new ProductStatusDto()
        {
            Id = result.Entity.Id,
            ProductStatusId = result.Entity.ProductStatusId,
            Name = result.Entity.Name
        };
    }

    public async Task<ProductStatusDto?> Show(Guid productStatusId)
    {
        return await QueryableProductStatusWithDefaultScopes()
            .Where(status => status.ProductStatusId == productStatusId).Select(status => new ProductStatusDto()
            {
                Id = status.Id,
                ProductStatusId = status.ProductStatusId,
                Name = status.Name
            }).FirstOrDefaultAsync();
    }

    public async Task<PaginatedResponseDto<ProductStatusDto>> GetAll(ProductStatusFilterDto filter)
    {
        var queryable = QueryableProductStatusWithDefaultScopes();
        var results = _paginationService.Paginate(queryable, filter);
        var statuses = await results.Select(status => new ProductStatusDto() 
        {
            Id = status.Id, 
            ProductStatusId = status.ProductStatusId, 
            Name = status.Name 
        }).ToListAsync();

        return new PaginatedResponseDto<ProductStatusDto>()
        {
            Page = filter.Page,
            Limit = filter.Limit,
            Results = statuses
        };
    }

    private IQueryable<ProductStatus> QueryableProductStatusWithDefaultScopes()
    {
        return QueryableProductStatus().AsNoTracking().OrderBy(status => status.Id);
    }

    private IQueryable<ProductStatus> QueryableProductStatus()
    {
        return _dataContext.ProductStatuses;
    }
}