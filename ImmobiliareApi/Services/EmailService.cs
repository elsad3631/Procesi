using ImmobiliareApi.Entities;
using ImmobiliareApi.Interfaces;
using ImmobiliareApi.Interfaces.IBusinessServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ImmobiliareApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailRegistration(string toEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress(_emailSettings.AdminEmail, "marcelle.labadie@ethereal.email"));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, false);
                    await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
                    await client.SendAsync(emailMessage);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }

        }

        public async Task NotifyAdminOfRegistration(string customerName, string customerLastName, string customerEmail)
        {
            var subject = "New Customer Registration";
            var message = $"A new customer has registered:\n\nName: {customerName}\nEmail: {customerLastName}\nEmail: {customerEmail}";
            await SendEmailRegistration(_emailSettings.AdminEmail, subject, message);
        }
    }
}
