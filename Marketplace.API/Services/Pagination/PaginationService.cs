using Marketplace.Dto;

namespace Marketplace.Services.Pagination;

public class PaginationService : IPaginationService
{
    public IQueryable<T> Paginate<T>(IQueryable<T> queryable, CollectionFilterDto filter)
    {
        var page = filter.Page <= 0 ? 1 : filter.Page;
        var limit = filter.Limit <= 0 ? 10 : filter.Limit;
        var offset = (page - 1) * limit;
        var paginated = queryable
            .Skip(offset)
            .Take(filter.Limit);

        return paginated;
    }
}
