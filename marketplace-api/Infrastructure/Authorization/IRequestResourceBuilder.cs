using marketplace_api.Dto;

namespace marketplace_api.Infrastructure.Authorization;

public interface IRequestResourceBuilder
{
    public IRequestPrincipal Build(UserIdentityDto user);
}