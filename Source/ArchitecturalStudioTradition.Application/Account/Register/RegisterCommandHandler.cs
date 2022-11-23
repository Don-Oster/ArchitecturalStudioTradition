using ArchitecturalStudioTradition.Contract.v1.Account;
using ArchitecturalStudioTradition.CQRS.Interfaces;
using ArchitecturalStudioTradition.Infrastructure.Authentication;
using ArchitecturalStudioTradition.Infrastructure.EmailManagement;
using ArchitecturalStudioTradition.Infrastructure.EmailManagement.Models;
using ArchitecturalStudioTradition.Model.UserIdentity;
using AutoMapper;
using Microsoft.AspNetCore.WebUtilities;

namespace ArchitecturalStudioTradition.Application.Account.Register
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, RegistrationResponse>
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IIdentityProvider identityProvider, IEmailSender emailSender, IMapper mapper)
        {
            _identityProvider = identityProvider;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        public async Task<RegistrationResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);

            var response = await _identityProvider.RegisterUserAsync(user, request.Password, new[] { Role.Viewer });
            if (response.IsSuccessful)
            {
                var twoFactorResponse = await _identityProvider.GenerateEmailConfirmationTokenAsync(request.Email);
                if (!twoFactorResponse.IsSuccessful)
                    return new RegistrationResponse().WithErrors(twoFactorResponse.Errors);

                var param = new Dictionary<string, string?>
                {
                    {"token", twoFactorResponse.Token },
                    {"email", user.Email }
                };

                var callback = QueryHelpers.AddQueryString(request.ClientUri, param);

                var message = new EmailMessage(new string[] { user.Email }, "Email Confirmation token", callback, null);
                
                await _emailSender.SendEmailAsync(message);
            }

            return response;
        }
    }
}
