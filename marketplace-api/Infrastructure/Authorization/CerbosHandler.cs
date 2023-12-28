using Cerbos.Sdk.Builder;
using Cerbos.Sdk.Utility;
using marketplace_api.Dto;
using marketplace_api.Infrastructure.Cerbos;
using marketplace_api.Infrastructure.ConfigurationOptions;
using marketplace_api.Services.UserService;
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

    public async Task<bool> Handle(string userEmail, string resource, params string[] actions)
    {
        var client = _cerbosProvider.Client();
        var user = await _userService.GetUser(userEmail);

        if (user is null)
        {
            return false;
        }

        var userRoles = await _userService.GetUserRoles(user.Id);
        var principalRoles = userRoles.Select(role => role.Name).ToArray();
        var requestResource = RequestPrincipal(user);

        var request = CheckResourcesRequest.NewInstance().WithRequestId(RequestId.Generate()).WithIncludeMeta(true)
            .WithPrincipal(
                Principal.NewInstance(requestResource.Id, principalRoles)
                    .WithPolicyVersion(requestResource.PolicyVersion)
            ).WithResourceEntries(
                ResourceEntry.NewInstance(resource, requestResource.Id)
                    .WithPolicyVersion(requestResource.PolicyVersion)
                    .WithActions(actions.ToArray())
            );
        

        var result = client.CheckResources(request).Find(requestResource.Id);

        bool isAllowed = actions.All(action => result.IsAllowed(action));

        if (isAllowed is false)
        {
            return false;
        }

        return true;
    }

    private IRequestPrincipal RequestPrincipal(UserIdentityDto user)
    {
        IRequestPrincipal result = new RequestPrincipal()
        {
            Id = Convert.ToString(user.Id),
            PolicyVersion = _options.PolicyVersion
        };

        return result;
    }
}