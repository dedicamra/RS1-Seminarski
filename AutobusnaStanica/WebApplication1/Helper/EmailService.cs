using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace WebApplication1.Helper
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
       // private readonly IWebHostEnvironment _enviroment;

        public EmailService(IOptions<SmtpSettings> smtpSettings)//, IWebHostEnvironment environment)
        {
            _smtpSettings = smtpSettings.Value;
            //_enviroment = environment;

        }



        public async Task SendEmailAsync(string email, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port);//, true);

                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }

        }
    }
}
