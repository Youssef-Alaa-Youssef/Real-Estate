using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Utils;
using RealEstate.PL.Helper;
using RealEstate.PL.Services.Email;

namespace RealEstate.PL.Services
{
    public class EmailSender : IEmailService
    {
        private readonly EmailConfiguration _emailSettings;

        public EmailSender(IOptions<EmailConfiguration> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile> attachments = null)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Email));
            email.To.Add(MailboxAddress.Parse(mailTo));
            email.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = body;

            // Add attachments if any
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    if (attachment.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await attachment.CopyToAsync(memoryStream);
                            memoryStream.Seek(0, SeekOrigin.Begin);

                            var attachmentName = Path.GetFileName(attachment.FileName);
                            builder.Attachments.Add(attachmentName, memoryStream.ToArray());
                        }
                    }
                }
            }

            email.Body = builder.ToMessageBody();

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.CheckCertificateRevocation = false; // You may adjust this as needed

                await smtpClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);

                await smtpClient.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);

                await smtpClient.SendAsync(email);

                await smtpClient.DisconnectAsync(true);
            }
        }
    }
}
