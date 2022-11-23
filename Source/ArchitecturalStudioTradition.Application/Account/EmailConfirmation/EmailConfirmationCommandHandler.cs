using ArchitecturalStudioTradition.Common.Exceptions;
using ArchitecturalStudioTradition.CQRS.Interfaces;
using ArchitecturalStudioTradition.Model.UserIdentity;
using Microsoft.AspNetCore.Identity;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    public class EmailConfirmationCommandHandler : ICommandHandler<EmailConfirmationCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public EmailConfirmationCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(EmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) throw new BadRequestException("Invalid request");

            var confirmResult = await _userManager.ConfirmEmailAsync(user, request.Token);
            if (!confirmResult.Succeeded)
            {
                throw new BadRequestException("Invalid request");
            }

            return confirmResult.Succeeded;
        }
    }
}
