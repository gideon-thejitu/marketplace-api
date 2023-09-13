using marketplace_api.Data;
using marketplace_api.Dto;
using marketplace_api.Helpers;
using marketplace_api.Models;

namespace marketplace_api.Services.RegistrationsService;

public class RegistrationsService :  IRegistrationService
{
    private readonly DataContext _dataContext;

    public RegistrationsService(DataContext context)
    {
        _dataContext = context;
    }
    public async Task<UserIdentityDto> Create(RegistrationCreateDto data)
    {
        var user = new UserIdentity()
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            PasswordHash = new BCryptHelper().Hash(data.Password)
        };

        _dataContext.UserIdentities.Add(user);

        await _dataContext.SaveChangesAsync();

        return new UserIdentityDto()
        {
            Id = user.Id,
            UserIdentityId = user.UserIdentityId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt,
        };
    }
}
