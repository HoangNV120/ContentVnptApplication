using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailService : IIdentityMessageService
{
    public Task SendAsync(IdentityMessage message)
    {
        var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential("yourgmail@gmail.com", "app_password")
        };

        var mail = new MailMessage
        {
            From = new MailAddress("yourgmail@gmail.com"),
            Subject = message.Subject,
            Body = message.Body,
            IsBodyHtml = true
        };

        mail.To.Add(message.Destination);

        return client.SendMailAsync(mail);
    }
}