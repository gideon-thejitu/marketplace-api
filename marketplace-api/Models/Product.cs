using System.Diagnostics.CodeAnalysis;

namespace marketplace_api.Models;

// Dependent(Child -> Product Status)
public class Product
{
    public long Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    [NotNull]
    public decimal Price { get; set; }
    public decimal DiscountedPrice { get; set; }
    public long ProductStatusId { get; set; }
    public ProductStatus ProductStatus { get; set; } = null!;
    public long CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}
