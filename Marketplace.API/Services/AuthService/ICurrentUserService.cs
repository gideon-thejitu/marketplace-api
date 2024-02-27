using Marketplace.Domain.Entities;

namespace Marketplace.Services.AuthService;

public interface ICurrentUserService
{
    public Task<UserIdentity> GetCurrentUser();
    public string? GetCurrentUserEmail();
}