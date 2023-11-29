using System.Security.Claims;
using marketplace_api.Services.UserService;
using Microsoft.AspNetCore.Authorization;

namespace marketplace_api.Infrastructure.Authorization;

public class AuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserService _userService;

    public AuthorizationHandler(IUserService userService)
    {
        _userService = userService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var claims = context.User.Claims;
        var sub = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

        if (sub is null)
        {
            context.Fail(new AuthorizationFailureReason(this, "Invalid!"));
        }
        
        var user = await _userService.

        context.Succeed(requirement);
    }
}
