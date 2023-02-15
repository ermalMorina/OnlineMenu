using OnlineMenu.Interfaces;
using System.Net;
using System.Net.Mail;

namespace OnlineMenu.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _config;

        public EmailRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message)
        {

            var mailMessage = new MailMessage(fromAddress, toAddress, subject, message);

            using (var client = new SmtpClient(_config["SMTP:Host"], int.Parse(_config["SMTP:Port"]))
            {
                Credentials = new NetworkCredential(_config["SMTP:Username"], _config["SMTP:Password"])
            })

            {
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}
