using marketplace_api.Dto;

namespace marketplace_api.Services.RegistrationsService;

public interface IRegistrationService
{
    public Task<UserIdentityDto> Create(RegistrationCreateDto data);
}
