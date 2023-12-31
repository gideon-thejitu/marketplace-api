using System.ComponentModel.DataAnnotations.Schema;
using marketplace_api.Data.Configurations;

namespace marketplace_api.Models;

[Table("UserIdentities")]
public class UserIdentity
{
    public long Id { get; set; }
    public Guid UserIdentityId { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string PasswordHash { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; } = new List<RefreshToken>();
    public ICollection<Notification> Notifications { get; } = new List<Notification>();
    public ICollection<UserIdentityRole> UserIdentityRoles { get; } = new List<UserIdentityRole>();
}
