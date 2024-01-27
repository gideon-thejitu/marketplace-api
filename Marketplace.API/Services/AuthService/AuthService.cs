using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Marketplace.Domain.Entities;
using Marketplace.Dto;
using Marketplace.Helpers;
using Marketplace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Marketplace.Services.AuthServiceService;

public class AuthService : IAuthService
{
    private readonly DataContext _dataContext;
    private readonly IConfiguration _configuration;

    public AuthService(DataContext dataContext, IConfiguration configuration)
    {
        _dataContext = dataContext;
        _configuration = configuration;
    }
    public async Task<AuthDto?> Create(AuthCreateDto data)
    {
        var user = await _dataContext
            .UserIdentities
            .FirstOrDefaultAsync(user => user.Email == data.Email);

        if (user is null)
        {
            return null;
        }

        var isPasswordMatch = new BCryptHelper().Verify(data.Password, user.PasswordHash);

        if (isPasswordMatch is false)
        {
            return null;
        }

        var tokens = Authenticate(user);

        await SaveToken(user, tokens);

        return tokens;
    }

    private AuthDto Authenticate(UserIdentity identity)
    {
        var issuer = _configuration["JWT:Issuer"];
        var audience = _configuration["JWT:Audience"];
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWT:Expiry")),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var refreshToken = GenerateRefreshToken();

        return new AuthDto()
        {
            JwtToken = jwtToken,
            RefreshToken = refreshToken
        };
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private async Task SaveToken(UserIdentity identity, AuthDto tokens)
    {
        identity.RefreshTokens.Add(new RefreshToken()
        {
            Token = tokens.RefreshToken,
            Expiry = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("JWT:RefreshTokenExpiry"))
        });

        await _dataContext.SaveChangesAsync();
    }
}