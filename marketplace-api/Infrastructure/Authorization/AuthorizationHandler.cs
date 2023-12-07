using System.Security.Claims;
using Cerbos.Sdk.Builder;
using Cerbos.Sdk.Utility;
using marketplace_api.Infrastructure.Cerbos;
using marketplace_api.Services.UserService;
using Microsoft.AspNetCore.Authorization;

namespace marketplace_api.Infrastructure.Authorization;

public class AuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ICerbosProvider _cerbosProvider;
    private readonly IRequestResourceBuilder _requestResourceBuilder;

    public AuthorizationHandler(IServiceScopeFactory serviceScopeFactory, ICerbosProvider cerbosProvider, IRequestResourceBuilder requestResourceBuilder)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _cerbosProvider = cerbosProvider;
        _requestResourceBuilder = requestResourceBuilder;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var client = _cerbosProvider.Client();
        using var factory = _serviceScopeFactory.CreateScope();
        var userService = factory.ServiceProvider.GetService<IUserService>();

        if (userService is null)
        {
            context.Fail(new AuthorizationFailureReason(this, "Service not found!"));
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

        var userRoles = await userService.GetUserRoles(user.UserIdentityId);
        var principalRoles = userRoles.Select(role => role.Name).ToArray();
        var requestResource = _requestResourceBuilder.Build(user);
        string resourceKind = requirement.Permission.Contains(".") ? requirement.Permission.Split(".")[0] : requirement.Permission;
        List<string> actions = new List<string>();

        string[] rawActions = requirement.Permission.Split(".");
        for (int i = 0; i < rawActions.Length; i++)
        {
            if (i != 0)
            {
                actions.Add(rawActions[i]);
            }
        }
        

        var request = CheckResourcesRequest.NewInstance().WithRequestId(RequestId.Generate()).WithIncludeMeta(true)
            .WithPrincipal(
                Principal.NewInstance(requestResource.Id, principalRoles)
                    .WithPolicyVersion(requestResource.PolicyVersion)
            ).WithResourceEntries(
                ResourceEntry.NewInstance(resourceKind, requestResource.Id)
                    .WithPolicyVersion(requestResource.PolicyVersion)
                    .WithActions(actions.ToArray())
                );
        

        var result = client.CheckResources(request).Find(requestResource.Id);

        bool isAuthorized = actions.All(action => result.IsAllowed(action));

        if (isAuthorized is false)
        {
            context.Fail(new AuthorizationFailureReason(this, "Unauthorized Action!"));
            return;
        }

        context.Succeed(requirement);
    }
}
