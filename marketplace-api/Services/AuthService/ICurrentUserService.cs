using marketplace_api.Models;

namespace marketplace_api.Services.Auth;

public interface ICurrentUserService
{
    public Task<UserIdentity> GetCurrentUser();
    public string? GetCurrentUserEmail();
}