using marketplace_api.Data;
using marketplace_api.Exceptions;
using marketplace_api.Models;
using marketplace_api.Services.MailService;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.Services.NotificationService;

public class NotificationService : INotificationService
{
    private readonly EmailService _emailService;
    private readonly DataContext _dataContext;

    public NotificationService(DataContext dataContext)
    {
        _dataContext = dataContext;
        _emailService = new EmailService();
    }

    public async Task Job()
    {
        var notifications = await _dataContext.Notifications.Include(notification => notification.UserIdentity).ToListAsync();

        foreach (var notification in notifications)
        { 
            await SendEmail(notification);
        }
    }

    public async Task SendEmail(Notification notification)
    {
        if (notification.IsSent is false)
        {
            try
            {
                var to = notification?.UserIdentity?.Email;
        
                _emailService.Send(to);
                Console.WriteLine("aaaa");
                await MarkSentNotification(notification);
            }
            catch (EmailNotSentException e)
            {
                Console.WriteLine("eee");
                await MarkSentNotification(notification);
                // throw;
            }
        }
    }

    private async Task MarkSentNotification(Notification notification)
    {
        notification.IsSent = true;
        await _dataContext.SaveChangesAsync();
    }
}
