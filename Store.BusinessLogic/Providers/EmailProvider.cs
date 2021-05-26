using MailKit.Net.Smtp;
using MimeKit;
using Store.BusinessLogic.Providers.Interfaces;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Helpers
{
    public class EmailProvider : IEmailProvider
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(Shared.Constants.DefaultValues.AdminRole, Shared.Constants.DefaultValues.EmailForEmailProvider));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com");
                await client.AuthenticateAsync(Shared.Constants.DefaultValues.EmailForEmailProvider, Shared.Constants.DefaultValues.EmailForEmailProvider);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }

        }
    }
}
