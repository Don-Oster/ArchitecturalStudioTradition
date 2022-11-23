using ArchitecturalStudioTradition.Contract.v1.Account;
using ArchitecturalStudioTradition.CQRS.Interfaces;
using ArchitecturalStudioTradition.Infrastructure.Authentication;
using ArchitecturalStudioTradition.Infrastructure.EmailManagement;
using ArchitecturalStudioTradition.Infrastructure.EmailManagement.Models;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, AuthenticationResponse>
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IEmailSender _emailSender;

        public LoginCommandHandler(IIdentityProvider identityProvider, IEmailSender emailSender)
        {
            _identityProvider = identityProvider;
            _emailSender = emailSender;
        }

        public async Task<AuthenticationResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityProvider.LoginUserAsync(request.Email, request.Password);
            if (response.IsLockedOut)
            {
                var content = $@"Your account is locked out. To reset the password click this link: {request.ClientUri}";
                var message = new EmailMessage(new string[] { request.Email }, "Locked out account information", content);

                await _emailSender.SendEmailAsync(message);
            }
           
            if (response.Is2StepVerificationRequired)
            {
                var twoFactorResponse = await _identityProvider.GenerateTwoFactorTokenAsync(request.Email, response.Provider);
                if (!twoFactorResponse.IsSuccessful)
                    return new AuthenticationResponse().WithErrors(twoFactorResponse.Errors);

                var message = new EmailMessage(new string[] { request.Email }, "Authentication token", twoFactorResponse.Token);

                await _emailSender.SendEmailAsync(message);
            }

            return response;
        }
    }
}
