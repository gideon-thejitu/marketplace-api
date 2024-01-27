using System.ComponentModel.DataAnnotations;

namespace Marketplace.Dto;

public class RegistrationCreateDto
{
    [Required]
    public string FirstName { get; set; } = String.Empty;
    [Required]
    public string LastName { get; set; } = String.Empty;
    [EmailAddress]
    public string Email { get; set; } = String.Empty;
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = String.Empty;
    [Compare("Password", ErrorMessage = "Password must match!")]
    public string ConfirmPassword { get; set; } = String.Empty;
}