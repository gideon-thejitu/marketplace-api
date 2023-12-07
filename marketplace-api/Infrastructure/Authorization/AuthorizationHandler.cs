using System.Security.Claims;
using marketplace_api.Services.UserService;
using Microsoft.AspNetCore.Authorization;

namespace marketplace_api.Infrastructure.Authorization;

public class AuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        using var factory = _serviceScopeFactory.CreateScope();
        var userService = factory.ServiceProvider.GetService<IUserService>();
        var cerbosHandler = factory.ServiceProvider.GetService<ICerbosHandler>();

        if (userService is null || cerbosHandler is null)
        {
            context.Fail(new AuthorizationFailureReason(this, "Required Services not found!"));
            return;
        }

        var claims = context.User.Claims;
        var sub = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;

        if (sub is null)
        {
            context.Fail(new AuthorizationFailureReason(this, "Invalid!"));
            return;
        }

        var user = await userService.GetUserByEmail(sub);

        if (user is null)
        {
            context.Fail(new AuthorizationFailureReason(this, "User not found!"));
            return;
        }
        
        await cerbosHandler.Handle(this, context, requirement, user);
    }
}
