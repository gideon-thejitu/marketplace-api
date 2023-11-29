using marketplace_api.Dto;

namespace marketplace_api.Services.RegistrationService;
public interface IRegistrationService
{
    public Task<UserIdentityDto> Create(RegistrationCreateDto data);
    public Task<bool> EmailTaken(string Email);
}
