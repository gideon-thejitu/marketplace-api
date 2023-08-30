using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace marketplace_api.Data.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .ToTable("Categories")
            .HasKey(t => t.Id);
        builder.Property(t => t.CategoryId)
            .HasDefaultValueSql("NEWID()");
        builder.HasMany<Product>(t => t.Products)
            .WithOne(t => t.Category)
            .HasForeignKey(product => product.CategoryId)
            .IsRequired();
    }
}