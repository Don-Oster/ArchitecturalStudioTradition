using ArchitecturalStudioTradition.Contract.v1.Account;
using ArchitecturalStudioTradition.CQRS.Interfaces;
using ArchitecturalStudioTradition.Infrastructure.Authentication;
using ArchitecturalStudioTradition.Infrastructure.EmailManagement;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    public class ExternalLoginCommandHandler : ICommandHandler<ExternalLoginCommand, AuthenticationResponse>
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IEmailSender _emailSender;

        public ExternalLoginCommandHandler(IIdentityProvider identityProvider, IEmailSender emailSender)
        {
            _identityProvider = identityProvider;
            _emailSender = emailSender;
        }

        public async Task<AuthenticationResponse> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
        {
            return new AuthenticationResponse();
        }
    }
}
