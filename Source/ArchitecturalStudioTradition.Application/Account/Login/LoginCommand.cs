using ArchitecturalStudioTradition.Contract.v1.Account;
using ArchitecturalStudioTradition.CQRS.Interfaces;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    public class LoginCommand : ICommand<AuthenticationResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ClientUri { get; set; }

    }
}
