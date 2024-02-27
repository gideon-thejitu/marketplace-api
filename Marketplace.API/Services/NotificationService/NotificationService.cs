using Marketplace.Domain.Entities;
using Marketplace.Exceptions;
using Marketplace.Infrastructure.Data;
using Marketplace.Services.Mail;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.NotificationService;

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
        var incompleteNotifications = await _dataContext.Notifications.Include(notification => notification.UserIdentity).Where(n => n.IsSent == false).ToListAsync();

        Console.WriteLine("Executing");
        Console.WriteLine($"Uncompleted Record {incompleteNotifications.Count}");
        
        var notifications = await _dataContext.Notifications.Include(notification => notification.UserIdentity).ToListAsync();

        foreach (var notification in notifications)
        { 
            await SendEmail(notification);
        }
        
        Console.WriteLine("Done");
    }

    public async Task SendEmail(Notification notification)
    {
        if (notification.IsSent is false)
        {
            var to = notification?.UserIdentity?.Email;

            try
            {
                await _emailService.Send(to);
                await MarkSentNotification(notification);
            }
            catch (EmailNotSentException e)
            {
                await MarkSentNotification(notification);
            }
        }
    }

    private async Task MarkSentNotification(Notification notification)
    {
        notification.IsSent = true;
        await _dataContext.SaveChangesAsync();
    }
}
