using System.Diagnostics.CodeAnalysis;

namespace marketplace_api.Models;

public class Product
{
    public long Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    [NotNull]
    public long ProductStatusId { get; set; }
    public ProductStatus ProductStatus { get; set; } = new ProductStatus();
    [NotNull]
    public long CategoryId { get; set; }
    public Category Category { get; set; } = new Category();
}
