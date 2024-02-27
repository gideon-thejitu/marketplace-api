namespace Marketplace.Domain.Entities;

public class Notification
{
    public long Id { get; set; }
    public long UserIdentityId { get; set; }
    public bool IsSent { get; set; } = false;
    public UserIdentity? UserIdentity { get; set; } = null!;
}
