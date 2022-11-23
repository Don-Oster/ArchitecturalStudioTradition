using ArchitecturalStudioTradition.Infrastructure.FileManagement.Models;
using MimeKit;

namespace ArchitecturalStudioTradition.Infrastructure.EmailManagement.Models
{
    public class EmailMessage
    {
        public IReadOnlyCollection<MailboxAddress> To { get; set; }
        public IReadOnlyCollection<MailboxAddress>? Bcc { get; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public IReadOnlyCollection<FileInformation>? Attachments { get; }

        public EmailMessage(IEnumerable<string> to, string subject, string content, IEnumerable<string>? bcc = null, IReadOnlyCollection<FileInformation>? attachments = null)
        {
            To = new List<MailboxAddress>(to.Select(x => new MailboxAddress("email", x)));
            Bcc = new List<MailboxAddress>(bcc == null ? Enumerable.Empty<MailboxAddress>() : bcc.Select(x => new MailboxAddress("email", x)));
            Subject = subject;
            Content = content;
            Attachments = attachments;
        }
    }
}
