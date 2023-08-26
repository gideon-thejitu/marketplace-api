namespace marketplace_api.Models;

public class ProductStatus
{
    public long Id { get; set; }
    public Guid ProductStatusId { get; set; }
    public string Name { get; set; } = null!;
    public IEnumerable<Product> Products { get; } = new List<Product>() { };
}
