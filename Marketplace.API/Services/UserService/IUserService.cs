using Marketplace.Domain.Entities;
using Marketplace.Dto;

namespace Marketplace.Services.UserService;

public interface IUserService
{
    public Task<PaginatedResponseDto<UserIdentityDto>> GetAllUsers(UserIdentityFilterDto query);
    public Task<UserIdentityDto?> GetUser(string email);
    public Task<UserIdentityDto?> GetUser(long id);
    public Task<ICollection<Role>> GetUserRoles(Guid userId);
    public Task<ICollection<Role>> GetUserRoles(long id);
    public Task<ICollection<Role>> GetUserRoles(string email);
}
