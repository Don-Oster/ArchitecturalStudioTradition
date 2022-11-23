using ArchitecturalStudioTradition.Infrastructure.EmailManagement.Models;
using MimeKit;

namespace ArchitecturalStudioTradition.Infrastructure.EmailManagement.Helpers
{
    internal static class MimeMessageBuilder
    {
        public static MimeMessage Build(string fromName, string fromAddress, EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(fromName, fromAddress));
            emailMessage.To.AddRange(message.To);
            emailMessage.Bcc.AddRange(message.Bcc);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = message.Content };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] bytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.FileContent.CopyTo(ms);
                        bytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, bytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }
    }
}
