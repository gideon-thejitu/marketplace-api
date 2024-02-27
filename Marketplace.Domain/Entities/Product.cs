using System.Diagnostics.CodeAnalysis;

namespace Marketplace.Domain.Entities;

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

    public long? CreatedById { get; set; }
    public UserIdentity? CreatedBy { get; set; }
    public long ProductStatusId { get; set; }
    public ProductStatus? ProductStatus { get; set; }
    public long CategoryId { get; set; }
    public Category? Category { get; set; }
    public DateTime? DeletedAt { get; set; }
}
