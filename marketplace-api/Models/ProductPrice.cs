using System.ComponentModel.DataAnnotations.Schema;

namespace marketplace_api.Models;

[Table("ProductPrices")]
public class ProductPrice
{
    public long Id { get; set; }
    public Guid ProductPriceId { get; set; }
    public decimal BasePrice { get; set; }
    public decimal DiscountAmount { get; set; } = 0;
    public long ProductId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; } = null;
    public DateTime? StartDate { get; set; } = null!;
    public DateTime? EndDate { get; set; } = null;
    public bool IsActive { get; set; } = false;
    public Product Product { get; set; } = null!;
}
