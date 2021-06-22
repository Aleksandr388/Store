using MailKit.Net.Smtp;
using MimeKit;
using Shared.Constants;
using Store.BusinessLogic.Providers.Interfaces;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Helpers
{
    public class EmailProvider : IEmailProvider
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(DefaultValues.AdminRole, DefaultValues.EmailForEmailProvider));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(DefaultValues.BaseConnectGmail);
                await client.AuthenticateAsync(DefaultValues.EmailForEmailProvider, DefaultValues.PasswordForEmailProvider);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
