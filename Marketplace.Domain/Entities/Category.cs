namespace Marketplace.Domain.Entities;

public class Category
{
    public long Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public ICollection<Product> Products { get; } = new List<Product>();
}
