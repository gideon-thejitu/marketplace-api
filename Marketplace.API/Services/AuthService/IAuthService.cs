using Marketplace.Dto;

namespace Marketplace.Services.AuthServiceService;

public interface IAuthService
{
    public Task<AuthDto?> Create(AuthCreateDto data);
}
