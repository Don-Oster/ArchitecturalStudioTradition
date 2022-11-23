using ArchitecturalStudioTradition.Common.Exceptions;
using ArchitecturalStudioTradition.Contract.v1.Account;
using ArchitecturalStudioTradition.CQRS.Interfaces;
using ArchitecturalStudioTradition.Infrastructure.Authentication;
using ArchitecturalStudioTradition.Model.UserIdentity;
using Microsoft.AspNetCore.Identity;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    public class TwoStepVerificationCommandHandler : ICommandHandler<TwoStepVerificationCommand, TokenResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly IIdentityProvider _identityProvider;

        public TwoStepVerificationCommandHandler(UserManager<User> userManager, IIdentityProvider identityProvider)
        {
            _userManager = userManager;
            _identityProvider = identityProvider;
        }

        public async Task<TokenResponse> Handle(TwoStepVerificationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) 
                throw new BadRequestException("Invalid request");

            var validVerification = await _userManager.VerifyTwoFactorTokenAsync(user, request.Provider, request.Token);
            if (!validVerification)
                throw new BadRequestException("Invalid token verification");

            return await _identityProvider.GenerateTwoFactorTokenAsync(request.Email, request.Provider);
        }
    }
}
