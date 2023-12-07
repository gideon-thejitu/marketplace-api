using marketplace_api.Dto;
using marketplace_api.Infrastructure.ConfigurationOptions;
using Microsoft.Extensions.Options;

namespace marketplace_api.Infrastructure.Authorization;

public class RequestResourceBuilder : IRequestResourceBuilder
{
    private readonly CerbosOptions _options;
    
    public RequestResourceBuilder(IOptions<CerbosOptions> options)
    {
        _options = options.Value;
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
