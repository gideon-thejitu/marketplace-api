using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace marketplace_api.Dto;

public class ProductDto
{
    public Guid ProductId { get; set; } = Guid.Empty;
    [Required]
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    [Required]
    public long ProductStatusId { get; set; }
    public long CategoryId { get; set; }
}
