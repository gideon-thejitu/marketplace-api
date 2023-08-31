namespace marketplace_api.Dto;

public class PaginatedResponseDto<TData>
{
    public int Page { get; set; }
    public int Limit { get; set; }
    public int Total { get; set; }
    public ICollection<TData> Results { get; set; } = new List<TData>();
}
