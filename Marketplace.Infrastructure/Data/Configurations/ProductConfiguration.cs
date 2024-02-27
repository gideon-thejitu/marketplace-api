using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Domain.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("Products")
            .HasKey(t => t.Id);
        builder.Property(t => t.ProductId)
            .HasDefaultValueSql("NEWID()");
        builder.Property(t => t.Price).HasPrecision(11, 2);
        builder.Property(t => t.DiscountedPrice).HasPrecision(11, 2);
        builder
            .HasOne<ProductStatus>(t => t.ProductStatus)
        .WithMany(t => t.Products)
        .HasForeignKey(t => t.ProductStatusId).IsRequired();
        builder.HasOne<Category>(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId);
        builder.HasOne<UserIdentity>(product => product.CreatedBy).WithMany(user => user.Products)
            .HasForeignKey(product => product.CreatedById);
    }
}
