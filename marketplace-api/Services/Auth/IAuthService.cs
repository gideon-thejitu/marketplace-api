using marketplace_api.Dto;

namespace marketplace_api.Services.Auth;

public interface IAuthService
{
    public Task<AuthDto?> Create(AuthCreateDto data);
}
