using ArchitecturalStudioTradition.Contract.v1.Account;
using ArchitecturalStudioTradition.CQRS.Interfaces;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    public class TwoStepVerificationCommand : ICommand<TokenResponse>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Provider { get; set; }

    }
}
