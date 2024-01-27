using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Infrastructure.Data.Configurations;

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