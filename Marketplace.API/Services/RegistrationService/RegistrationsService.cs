using Marketplace.Domain.Data;
using Marketplace.Domain.Entities;
using Marketplace.Dto;
using Marketplace.Helpers;
using Marketplace.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.RegistrationService;

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

    private IQueryable<UserIdentity> UserQueryable()
    {
        return _dataContext.UserIdentities;
    }

    public async Task<bool> EmailTaken(string email)
    {
        return await this.UserQueryable().AsNoTracking().AnyAsync(user => user.Email.ToLower() == email.ToLower());
    }
}
