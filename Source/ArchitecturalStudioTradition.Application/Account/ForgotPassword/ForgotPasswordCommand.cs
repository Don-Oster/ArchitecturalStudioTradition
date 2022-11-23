using ArchitecturalStudioTradition.CQRS.Interfaces;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    public class ForgotPasswordCommand : ICommand<string>
    {
        public string Email { get; set; }
        public string? ClientUri { get; set; }

    }
}
