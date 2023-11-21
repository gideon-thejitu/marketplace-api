using marketplace_api.Dto;

namespace marketplace_api.Services.UsersService;

public interface IUserService
{
    public Task<PaginatedResponseDto<UserIdentityDto>> GetAllUsers(UserIdentityFilterDto query);
}
