using System.Data.Entity.Core.Objects;
using marketplace_api.Data.Configurations;
using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {}
    
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductStatus> ProductStatuses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<UserIdentity> UserIdentities { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductStatusConfiguration());
        modelBuilder.ApplyConfiguration(new UserIdentityConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
    }
}
