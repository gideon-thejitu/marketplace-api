using marketplace_api.Data;
using marketplace_api.Dto;
using marketplace_api.Models;
using marketplace_api.Services.Pagination;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.Services.UserService;

public class UserService : IUserService
{
    private readonly DataContext _context;
    private readonly IPaginationService _paginationService;

    public UserService(DataContext context, IPaginationService paginationService)
    {
        _context = context;
        _paginationService = paginationService;
    }

    public async Task<PaginatedResponseDto<UserIdentityDto>> GetAllUsers(UserIdentityFilterDto query)
    {
        var usersQueryable = UserIdentityQueryable().AsNoTracking().OrderBy(user => user.Id);
        var total = await usersQueryable.CountAsync();
        var paginated =  await _paginationService.Paginate(usersQueryable, query).Select(user => new UserIdentityDto()
        {
            Id = user.Id,
            UserIdentityId = user.UserIdentityId,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        }).ToListAsync();
        
        throw new ArgumentException("This is intentional");

        return new PaginatedResponseDto<UserIdentityDto>()
        {
            Page = query.Page,
            Limit = query.Limit,
            Total = total,
            Results = paginated
        };
    }
    
    private IQueryable<UserIdentity> UserIdentityQueryable()
    {
        return _context.UserIdentities;
    }
}