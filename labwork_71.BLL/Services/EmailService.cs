using labwork_71.BLL.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace labwork_71.BLL.Services;

public class EmailService : IEmailService
{
    public async Task SendMessageAsync(string email, string subject, string body)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("FoodOrders", "beknazar.nogoybekov05@gmail.com"));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync( "beknazar.nogoybekov05@gmail.com", "gvfs uyqw pqai bmdh");
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}