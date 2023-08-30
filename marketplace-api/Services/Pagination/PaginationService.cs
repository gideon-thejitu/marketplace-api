using marketplace_api.Dto;

namespace marketplace_api.Services.Pagination;

public class PaginationService : IPaginationService
{
    public IQueryable<T> Paginate<T>(IQueryable<T> queryable, CollectionFilterDto filter)
    {
        var page = filter.Page;
        var offset = (page - 1) * filter.Limit;
        var paginated = queryable
            .Skip(offset)
            .Take(filter.Limit);

        return paginated;
    }
}
