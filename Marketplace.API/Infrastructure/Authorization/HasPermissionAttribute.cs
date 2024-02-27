using Microsoft.AspNetCore.Authorization;

namespace Marketplace.Infrastructure.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public string Attributes;
    public HasPermissionAttribute(string attributes) : base(policy: attributes)
    {
        Attributes = attributes;
    }
}