using System.Net;
using System.Net.Mail;

namespace marketplace_api.Services.MailService;

public class EmailService : IEmailService
{
    private readonly MailMessage email;
    private readonly SmtpClient client;

    public EmailService()
    {
        email = new MailMessage();
        client = new SmtpClient("sandbox.smtp.mailtrap.io");
        email.From = new MailAddress("marketplace.dev@mail.com", "Market Place Dev");
        client.EnableSsl = true;
        client.Port = 25;
        client.Credentials = new NetworkCredential("06e558847d688d", "a2d4998d05b50a");
    }

    public void Send(string to)
    {
        try
        {
            email.To.Add(to);
            email.Subject = "Market Place Dev";
            email.IsBodyHtml = true;
            email.Body = $"<h1>Hello {to}</h1>";
            client.Send(email);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}