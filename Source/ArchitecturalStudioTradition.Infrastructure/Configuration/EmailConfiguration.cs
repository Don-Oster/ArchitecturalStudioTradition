namespace ArchitecturalStudioTradition.Infrastructure.Configuration
{
    public interface IEmailConfiguration
    {
        string EmailFromName { get; }
        string EmailFromAddress { get; }
        string EmailSmtpServer { get; }
        int EmailPort { get; }
        string EmailUserName { get; }
        string EmailPassword { get;  }
    }
}
