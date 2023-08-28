using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace marketplace_api.Data.Configurations;

public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        builder
            .ToTable("ProductPrices")
            .HasKey(t => t.Id);
        builder.Property(t => t.ProductPriceId)
            .HasDefaultValueSql("NEWID()");
        builder.Property(productPrice => productPrice.CreatedAt)
            .HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
        builder.Property(productPrice => productPrice.UpdatedAt)
            .HasDefaultValueSql("GETDATE()").ValueGeneratedOnUpdate();
        builder.Property(productPrice => productPrice.BasePrice).HasPrecision(11, 2);
        builder.Property(productPrice => productPrice.DiscountAmount).HasPrecision(11, 2);
        builder.HasOne<Product>(productPrice => productPrice.Product)
            .WithMany(product => product.ProductPrices)
            .HasForeignKey(productPrice => productPrice.ProductId);
    }
}