using ArchitecturalStudioTradition.Common.Exceptions;
using ArchitecturalStudioTradition.CQRS.Interfaces;
using ArchitecturalStudioTradition.Infrastructure.Authentication;
using ArchitecturalStudioTradition.Infrastructure.EmailManagement;
using ArchitecturalStudioTradition.Infrastructure.EmailManagement.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    public class ForgotPasswordCommandHandler : ICommandHandler<ForgotPasswordCommand, string>
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordCommandHandler(IIdentityProvider identityProvider, IEmailSender emailSender)
        {
            _identityProvider = identityProvider;
            _emailSender = emailSender;
        }

        public async Task<string> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var response = await _identityProvider.GeneratePasswordResetTokenAsync(request.Email);
            if (!response.IsSuccessful)
                throw new BadRequestException("Invalid request");

            var param = new Dictionary<string, string?>
            {
                {"token", response.Token },
                {"email", request.Email }
            };

            var callback = QueryHelpers.AddQueryString(request.ClientUri, param);
            var message = new EmailMessage(new string[] { request.Email }, "Reset password token", callback);

            await _emailSender.SendEmailAsync(message);

            return response.Token;
        }
    }
}
