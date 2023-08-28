namespace marketplace_api.Dto;

public class PaginatedResponseDto<TData>
{
    public int Page { get; set; }
    public int Limit { get; set; }
    public ICollection<TData> Results { get; set; } = new List<TData>();

    public PaginatedResponseDto()
    {
        Page = Page > 0 ? Page : 1;
        Limit = Limit > 0 ? Limit : 100;
    }
}
