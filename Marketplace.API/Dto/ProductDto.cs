using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Marketplace.Dto;

public class ProductDto
{
    public long Id { get; set; }
    public Guid ProductId { get; set; }
    [Required]
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public decimal Price { get; set; } = Decimal.Zero;
    public decimal DiscountedPrice { get; set; } = Decimal.Zero;
    public DateTime? DeletedAt { get; set; } = null;
    public long? CreatedById { get; set; }
    public UserIdentityDto? CreatedBy { get; set; }
    public long ProductStatusId { get; set; }
    public ProductStatusDto? ProductStatus { get; set; }
    public long CategoryId { get; set; }
    public CategoryDto? Category { get; set; }

}

public class ProductCreateDto
{
    [Required]
    public string Name { get; set; } = String.Empty;
    [Required]
    public string Description { get; set; } = String.Empty;
    [Required]
    
    public long CreatedById { get; set; }

    public long ProductStatusId { get; set; }
    [Required]
    public long CategoryId { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public decimal DiscountedPrice { get; set; }
}

public class ProductUpdateDto
{
    [AllowNull]
    public string Name { get; set; } = String.Empty;
    [AllowNull]
    public string Description { get; set; } = String.Empty;
    public long? ProductStatusId { get; set; }
    public long? CategoryId { get; set; }
    public decimal? Price { get; set; }
    public decimal? DiscountedPrice { get; set; }
}

public class ProductStatusDto
{
    public long Id { get; set; } = 0;
    public Guid ProductStatusId { get; set; } = Guid.Empty;
    public string Name { get; set; } = String.Empty;
}

public class ProductStatusCreateDto
{
    [Required]
    public string Name { get; set; } = String.Empty;
}

public class ProductStatusFilterDto : CollectionFilterDto
{
    public ProductStatusFilterDto()
    {
        Limit = 100;
    }
}

public class ProductFilterDto : CollectionFilterDto
{
    public ProductFilterDto()
    {
        Limit = 100;
    }
}
