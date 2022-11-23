using ArchitecturalStudioTradition.Infrastructure.Configuration;
using MailKit.Net.Smtp;

namespace ArchitecturalStudioTradition.Infrastructure.EmailManagement
{
    internal interface ISmtpClientFactory
    {
        ISmtpClient Create(IEmailConfiguration config);
    }

    internal class SmtpClientFactory : ISmtpClientFactory
    {
        public ISmtpClient Create(IEmailConfiguration config)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Connect(config.EmailSmtpServer, config.EmailPort);
            smtpClient.Authenticate(config.EmailUserName, config.EmailPassword);

            return smtpClient;
        }
    }
}
