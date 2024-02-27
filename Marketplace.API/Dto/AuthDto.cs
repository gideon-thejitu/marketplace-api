using System.ComponentModel.DataAnnotations;

namespace Marketplace.Dto;

public class AuthDto
{
    public string JwtToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}

public class AuthCreateDto
{
    [EmailAddress] [Required] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}
