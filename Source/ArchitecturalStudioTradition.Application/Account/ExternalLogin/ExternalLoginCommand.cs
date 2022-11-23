using ArchitecturalStudioTradition.Contract.v1.Account;
using ArchitecturalStudioTradition.CQRS.Interfaces;
using ArchitecturalStudioTradition.Model.UserIdentity;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    public class ExternalLoginCommand : ICommand<AuthenticationResponse>
    {
        public ExternalAuthProvider? Provider { get; set; }
        public string? IdToken { get; set; }

    }
}
