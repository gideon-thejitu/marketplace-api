using Marketplace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Marketplace.Domain.Data.Configurations;

public class RequestLogConfiguration : IEntityTypeConfiguration<RequestLog>
{
    public void Configure(EntityTypeBuilder<RequestLog> builder)
    {
        builder
            .ToTable("RequestLogs")
            .HasKey(t => t.Id);
        builder.Property(t => t.RequestLogId)
            .HasDefaultValueSql("NEWID()");
    }
}