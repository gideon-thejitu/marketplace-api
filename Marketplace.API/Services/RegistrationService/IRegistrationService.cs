using Marketplace.Dto;

namespace Marketplace.Services.RegistrationService;
public interface IRegistrationService
{
    public Task<UserIdentityDto> Create(RegistrationCreateDto data);
    public Task<bool> EmailTaken(string Email);
}
