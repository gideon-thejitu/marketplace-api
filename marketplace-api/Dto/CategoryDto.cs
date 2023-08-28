using System.ComponentModel.DataAnnotations;

namespace marketplace_api.Dto;

public class CategoryDto
{
    public long Id { get; set; }
    public Guid CategoryId { get; set; } = Guid.Empty;
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
}

public class CategoryCreateDto
{
    [Required(ErrorMessage = "Category name is required")]
    public string Name { get; set; } = String.Empty;

    public string? Description { get; set; } = String.Empty;
}
