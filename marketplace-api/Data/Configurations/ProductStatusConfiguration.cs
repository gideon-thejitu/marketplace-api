using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace marketplace_api.Data.Configurations;

public class ProductStatusConfiguration : IEntityTypeConfiguration<ProductStatus>
{
    public void Configure(EntityTypeBuilder<ProductStatus> builder)
    {
        builder
            .ToTable("ProductStatuses")
            .HasKey(t => t.Id);
        builder
            .Property(t => t.ProductStatusId)
            .HasDefaultValueSql("NEWID()");
        builder
            .HasMany(t => t.Products)
            .WithOne(t => t.ProductStatus)
            .HasForeignKey(t => t.ProductStatusId)
            .IsRequired();
    }
}
