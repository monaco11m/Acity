using Microsoft.Extensions.Configuration;
using Notification.Application.Interfaces;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Notification.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Acity Notifier", _config["Email:From"]));
            email.To.Add(new MailboxAddress("", to));
            email.Subject = subject;
            email.Body = new TextPart("plain") { Text = message };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config["Email:SmtpHost"], int.Parse(_config["Email:SmtpPort"]), false);
            await smtp.AuthenticateAsync(_config["Email:User"], _config["Email:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
