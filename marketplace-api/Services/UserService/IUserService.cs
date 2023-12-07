using marketplace_api.Dto;
using marketplace_api.Models;

namespace marketplace_api.Services.UserService;

public interface IUserService
{
    public Task<PaginatedResponseDto<UserIdentityDto>> GetAllUsers(UserIdentityFilterDto query);
    public Task<UserIdentityDto?> GetUserByEmail(string email);
    public Task<ICollection<Role>> GetUserRoles(Guid userIdentityId);
}
