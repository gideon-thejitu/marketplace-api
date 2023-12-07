using Microsoft.AspNetCore.Authorization;

namespace marketplace_api.Infrastructure.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public string Attributes;
    public HasPermissionAttribute(string attributes) : base(policy: attributes)
    {
        Attributes = attributes;
    }
}