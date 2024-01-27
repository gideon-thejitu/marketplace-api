using Marketplace.Domain.Entities;
using Marketplace.Dto;
using Marketplace.Infrastructure.Data;
using Marketplace.Services.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.UserService;

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

        return new PaginatedResponseDto<UserIdentityDto>()
        {
            Page = query.Page,
            Limit = query.Limit,
            Total = total,
            Results = paginated
        };
    }

    public async Task<UserIdentityDto?> GetUser(string email)
    {
        var user = await UserIdentityQueryable().AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);

        if (user is null)
        {
            return null;
        }
        
        return ToDto(user);
    }

    public Task<UserIdentityDto?> GetUser(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Role>> GetUserRoles(Guid userIdentityId)
    {
        var user = await UserIdentityQueryable().AsNoTracking().Where(user => user.UserIdentityId == userIdentityId)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return new List<Role>();
        }

        var roles = user.UserIdentityRoles.Select(userIdentityRole => userIdentityRole.Role).ToList();

        return roles;
    }

    public async Task<ICollection<Role>> GetUserRoles(long id)
    {
        var user = await UserIdentityQueryable().AsNoTracking().Where(user => user.Id == id)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return new List<Role>();
        }

        var roles = user.UserIdentityRoles.Select(userIdentityRole => userIdentityRole.Role).ToList();

        return roles;
    }

    public async Task<ICollection<Role>> GetUserRoles(string email)
    {
        var user = await UserIdentityQueryable().AsNoTracking().Where(user => user.Email == email)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return new List<Role>();
        }

        var roles = user.UserIdentityRoles.Select(userIdentityRole => userIdentityRole.Role).ToList();

        return roles;
    }

    private UserIdentityDto ToDto(UserIdentity user)
    {
        return new UserIdentityDto()
        {
            Id = user.Id,
            UserIdentityId = user.UserIdentityId,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    private IQueryable<UserIdentity> UserIdentityQueryable()
    {
        return _context.UserIdentities.Include(user => user.UserIdentityRoles)
            .ThenInclude(userIdentityRole => userIdentityRole.Role);
    }
}