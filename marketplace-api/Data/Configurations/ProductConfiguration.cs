using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace marketplace_api.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("Products")
            .HasKey(t => t.Id);
        builder.Property(t => t.ProductId)
            .HasDefaultValueSql("NEWID()");
        builder
            .HasOne<ProductStatus>(t => t.ProductStatus)
            .WithMany(t => t.Products);
        builder.HasOne<Category>(product => product.Category)
            .WithMany(category => category.Products)
            .HasForeignKey(product => product.CategoryId);
        builder.HasMany<ProductPrice>(product => product.ProductPrices)
            .WithOne(productPrice => productPrice.Product);
    }
}
