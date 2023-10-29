using HairSalon.Web.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace HairSalon.Web.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            EmailSettings = emailSettings.Value;
        }

        public EmailSettings EmailSettings { get; }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(EmailSettings.Host, EmailSettings.Port)
            {
                Credentials = new NetworkCredential(EmailSettings.Email, EmailSettings.Password),
                EnableSsl = true,
            };

            await client.SendMailAsync(
                new MailMessage(EmailSettings.Email, email, subject, htmlMessage)
                {
                    IsBodyHtml = true
                });
        }
    }
}
