using System.Security.Claims;
using Marketplace.Domain.Data;
using Marketplace.Domain.Entities;
using Marketplace.Exceptions;
using Marketplace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.AuthService;

public class CurrentUserService : ICurrentUserService
{
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, DataContext dataContext)
    {
        _dataContext = dataContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserIdentity> GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            throw new MarketplaceException($"{nameof(HttpContext)} does not exist!");
        }

        var sub = GetCurrentUserEmail();

        if (sub is null)
        {
            throw new MarketplaceException("User claims does not exist!");
        }

        var user = await _dataContext.UserIdentities.Where(user => user.Email == sub).FirstOrDefaultAsync();

        if (user is null)
        {
            throw new MarketplaceException($"User with email = {sub} does not exist!");
        }

        return user;
    }

    public string? GetCurrentUserEmail()
    {
        var context = _httpContextAccessor.HttpContext;

        IEnumerable<Claim>? claims = context?.User.Claims;

        return claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
