using ArchitecturalStudioTradition.Infrastructure.Configuration;
using ArchitecturalStudioTradition.Infrastructure.EmailManagement.Helpers;
using ArchitecturalStudioTradition.Infrastructure.EmailManagement.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace ArchitecturalStudioTradition.Infrastructure.EmailManagement
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message);
    }

    internal class EmailSender : IEmailSender, IDisposable
    {
        private readonly IEmailConfiguration _configuration;
        private readonly ILogger<EmailSender> _logger;
        private readonly ISmtpClientFactory _smtpClientFactory;
        private readonly Lazy<ISmtpClient> _smtpClient;

        public EmailSender(IEmailConfiguration configuration, ISmtpClientFactory smtpClientFactory, ILogger<EmailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _smtpClientFactory = smtpClientFactory;
            _smtpClient = new Lazy<ISmtpClient>(InitSmtpClient);
        }
        public async Task SendEmailAsync(EmailMessage message)
        {
            var emailMessage = MimeMessageBuilder.Build(_configuration.EmailFromName, _configuration.EmailFromAddress, message);
            await SendAsync(emailMessage);
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            try
            {
                await _smtpClient.Value.SendAsync(mailMessage);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Failed to send email message to {mailMessage.To.First()}.", exception);
                throw;
            }
        }

        private ISmtpClient InitSmtpClient()
        {
            try
            {
                return _smtpClientFactory.Create(_configuration);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Failed to connect to SMTP server. Server: {_configuration.EmailSmtpServer}, Port: {_configuration.EmailPort}", exception);
                Dispose();
                throw;
            }
        }

        public void Dispose()
        {
            if (_smtpClient.IsValueCreated)
            {
                _smtpClient.Value.Disconnect(true);
                _smtpClient.Value.Dispose();
            }
        }
    }
}
