using marketplace_api.Dto;
using Microsoft.AspNetCore.Authorization;

namespace marketplace_api.Infrastructure.Authorization;

public interface ICerbosHandler
{
    public Task Handle(IAuthorizationHandler handler, AuthorizationHandlerContext context, PermissionRequirement requirement, UserIdentityDto user);
    public IRequestPrincipal Build(UserIdentityDto user);
}