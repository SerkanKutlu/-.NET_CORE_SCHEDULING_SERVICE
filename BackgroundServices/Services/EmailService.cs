using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Hosting;

namespace BackgroundServices.Services;

public class EmailService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var smtpClient = new SmtpClient("smtp-mail.outlook.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("developerserkankutlu@outlook.com", "155202Asd.."),
            EnableSsl = true,
        };
        
        var mailMessage = new MailMessage
        {
            From = new MailAddress("developerserkankutlu@outlook.com"),
            Subject = "subject",
            Body = "<h1>Hello</h1>",
            IsBodyHtml = true,
        };
        mailMessage.To.Add("kutluserkan1@gmail.com");
        await smtpClient.SendMailAsync(mailMessage, stoppingToken);
    }
}