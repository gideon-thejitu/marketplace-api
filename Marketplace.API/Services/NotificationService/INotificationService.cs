using Marketplace.Domain.Entities;

namespace Marketplace.Services.NotificationService;
public interface INotificationService
{
    public Task SendEmail(Notification notification);
    public Task Job();
}