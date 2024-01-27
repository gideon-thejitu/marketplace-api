using Marketplace.Domain.Data.Configurations;

namespace Marketplace.Domain.Entities;

public class Role
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = String.Empty;

    public ICollection<UserIdentityRole> UserIdentityRoles { get; } = new List<UserIdentityRole>();
}
