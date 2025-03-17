using MailKit.Net.Smtp;
using MimeKit;
using OrderTrackingSystem.Services.Interfaces;
<<<<<<< HEAD
=======
using System.Net.Mail;
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
using System.Threading.Tasks;

namespace OrderTrackingSystem.Services
{
    public class EmailService : IEmailService
    {
<<<<<<< HEAD
=======
        // Przykładowa konfiguracja SMTP – zastąp odpowiednimi wartościami
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        private readonly string _smtpServer = "smtp.example.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "user@example.com";
        private readonly string _smtpPass = "password";

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Order Tracking System", _smtpUser));
            emailMessage.To.Add(new MailboxAddress("", toEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

<<<<<<< HEAD
            using var client = new SmtpClient();
=======
            using var client = new MailKit.Net.Smtp.SmtpClient();
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
            await client.ConnectAsync(_smtpServer, _smtpPort, false);
            await client.AuthenticateAsync(_smtpUser, _smtpPass);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
