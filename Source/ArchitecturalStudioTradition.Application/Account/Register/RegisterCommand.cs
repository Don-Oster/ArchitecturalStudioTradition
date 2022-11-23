using ArchitecturalStudioTradition.Contract.v1.Account;
using ArchitecturalStudioTradition.CQRS.Interfaces;

namespace ArchitecturalStudioTradition.Application.Account.Register
{
    public class RegisterCommand : ICommand<RegistrationResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ClientUri { get; set; }

    }
}
