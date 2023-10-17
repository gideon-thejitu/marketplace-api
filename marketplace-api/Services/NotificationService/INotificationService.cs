using marketplace_api.Models;

namespace marketplace_api.Services.NotificationService;
public interface INotificationService
{
    public Task SendEmail(Notification notification);
    public Task Job();
}