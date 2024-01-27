namespace Marketplace.Infrastructure.Authorization;

public class RequestPrincipal : IRequestPrincipal
{
    public string Id { get; set; }
    public string PolicyVersion { get; set; } = String.Empty;
}
