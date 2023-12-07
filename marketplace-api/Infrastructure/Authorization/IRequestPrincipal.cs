namespace marketplace_api.Infrastructure.Authorization;

public interface IRequestPrincipal
{
    public string Id { get; set; }
    public string PolicyVersion { get; set; }
}