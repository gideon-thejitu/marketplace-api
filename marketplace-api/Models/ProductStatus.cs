namespace marketplace_api.Models;

// Principal(parent) to product
public class ProductStatus
{
    public long Id { get; set; }
    public Guid ProductStatusId { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Product> Products { get; } = new List<Product>();
}
