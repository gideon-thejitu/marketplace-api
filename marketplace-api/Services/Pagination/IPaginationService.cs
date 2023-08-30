using marketplace_api.Dto;

namespace marketplace_api.Services.Pagination;

public interface IPaginationService
{
    public IQueryable<T> Paginate<T>(IQueryable<T> queryable, CollectionFilterDto filter);
}
