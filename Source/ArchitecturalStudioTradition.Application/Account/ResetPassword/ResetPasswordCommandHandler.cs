using ArchitecturalStudioTradition.Common.Exceptions;
using ArchitecturalStudioTradition.CQRS.Interfaces;
using ArchitecturalStudioTradition.Model.UserIdentity;
using Microsoft.AspNetCore.Identity;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public ResetPasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new BadRequestException("Invalid request");

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
                throw new BadRequestException(string.Join(',', result.Errors.Select(e => e.Description)));

            await _userManager.SetLockoutEndDateAsync(user, new DateTime(2000, 1, 1));

            return result.Succeeded;
        }
    }
}
