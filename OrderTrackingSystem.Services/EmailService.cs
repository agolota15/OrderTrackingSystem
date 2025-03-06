using MailKit.Net.Smtp;
using MimeKit;
using OrderTrackingSystem.Services.Interfaces;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OrderTrackingSystem.Services
{
    public class EmailService : IEmailService
    {
        // Przykładowa konfiguracja SMTP – zastąp odpowiednimi wartościami
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

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_smtpServer, _smtpPort, false);
            await client.AuthenticateAsync(_smtpUser, _smtpPass);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
