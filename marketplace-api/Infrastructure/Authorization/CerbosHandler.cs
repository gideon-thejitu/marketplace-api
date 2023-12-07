using Cerbos.Sdk.Builder;
using Cerbos.Sdk.Utility;
using marketplace_api.Dto;
using marketplace_api.Infrastructure.Cerbos;
using marketplace_api.Infrastructure.ConfigurationOptions;
using marketplace_api.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace marketplace_api.Infrastructure.Authorization;

public class CerbosHandler : ICerbosHandler
{
    private readonly ICerbosProvider _cerbosProvider;
    private readonly IUserService _userService;
    private readonly CerbosOptions _options;

    public CerbosHandler(ICerbosProvider cerbosProvider, IUserService userService, IOptions<CerbosOptions> options)
    {
        _cerbosProvider = cerbosProvider;
        _userService = userService;
        _options = options.Value;
    }

    public async Task Handle(IAuthorizationHandler handler, AuthorizationHandlerContext context, PermissionRequirement requirement, UserIdentityDto user)
    {
        var client = _cerbosProvider.Client();
        var userRoles = await _userService.GetUserRoles(user.UserIdentityId);
        var principalRoles = userRoles.Select(role => role.Name).ToArray();
        var requestResource = Build(user);
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
            context.Fail(new AuthorizationFailureReason(handler, "Unauthorized Action!"));
            return;
        }

        context.Succeed(requirement);
    }

    public IRequestPrincipal Build(UserIdentityDto user)
    {
        IRequestPrincipal result = new RequestPrincipal()
        {
            Id = Convert.ToString(user.Id),
            PolicyVersion = _options.PolicyVersion
        };

        return result;
    }
}