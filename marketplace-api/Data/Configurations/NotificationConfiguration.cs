namespace marketplace_api.Data.Configurations;


using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder
            .ToTable("Notifications")
            .HasKey(t => t.Id);
        builder.HasOne<UserIdentity>(t => t.UserIdentity)
            .WithMany(t => t.Notifications)
            .HasForeignKey(notification => notification.UserIdentityId)
            .IsRequired();
    }
}