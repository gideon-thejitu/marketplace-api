using marketplace_api.Dto;

namespace marketplace_api.Services.UserService;

public interface IUserService
{
    public Task<PaginatedResponseDto<UserIdentityDto>> GetAllUsers(UserIdentityFilterDto query);
    public Task<UserIdentityDto?> GetUserByEmail(string email);
}
