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
    public DbSet<ProductPrice> ProductPrices { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductStatusConfiguration());
        modelBuilder.ApplyConfiguration(new ProductPriceConfiguration());
    }
}
