using ArchitecturalStudioTradition.Application.Authentication;
using ArchitecturalStudioTradition.Common.Extensions;
using ArchitecturalStudioTradition.Contract.v1.Account;
using ArchitecturalStudioTradition.Model.UserIdentity;
using Microsoft.AspNetCore.Identity;

namespace ArchitecturalStudioTradition.Infrastructure.Authentication
{
    public interface IIdentityProvider
    {
        Task<AuthenticationResponse> LoginUserAsync(string email, string password);
        Task<RegistrationResponse> RegisterUserAsync(User user, string password, IReadOnlyCollection<Role> roles);
        Task<TokenResponse> GenerateTwoFactorTokenAsync(string email, string provider);
        Task<TokenResponse> GenerateEmailConfirmationTokenAsync(string email);
        Task<TokenResponse> GeneratePasswordResetTokenAsync(string email);
    }

    internal class IdentityProvider : IIdentityProvider
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthTokenGenerator _authTokenGenerator;

        public IdentityProvider(UserManager<User> userManager, IAuthTokenGenerator authTokenGenerator)
        {
            _userManager = userManager;
            _authTokenGenerator = authTokenGenerator;
        }

        public async Task<AuthenticationResponse> LoginUserAsync(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
                return new AuthenticationResponse().WithErrors("Invalid request");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return new AuthenticationResponse().WithErrors("Email is not confirmed");

            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                await _userManager.AccessFailedAsync(user);

                if (await _userManager.IsLockedOutAsync(user))
                    return new AuthenticationResponse { IsLockedOut = true }.WithErrors("The account is locked out");

                return new AuthenticationResponse().WithErrors("Invalid authentication");
            }

            if (await _userManager.GetTwoFactorEnabledAsync(user))
            {
                var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
                if (!providers.Contains("Email"))
                    return new AuthenticationResponse().WithErrors("Invalid 2-Step Verification Provider.");

                return new AuthenticationResponse() { Is2StepVerificationRequired = true, Provider = "Email" };
            }

            var token = await _authTokenGenerator.GenerateJwtTokenAsync(user);

            await _userManager.ResetAccessFailedCountAsync(user);

            return new AuthenticationResponse() { IsSuccessful = true, Token = token };
        }

        public async Task<RegistrationResponse> RegisterUserAsync(User user, string password, IReadOnlyCollection<Role> roles)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToArray();
                return new RegistrationResponse().WithErrors(errors);
            }

            foreach (var role in roles)
                await _userManager.AddToRoleAsync(user, role.GetDescription());

            return new RegistrationResponse() { IsSuccessful = true };
        }

        public async Task<TokenResponse> GenerateTwoFactorTokenAsync(string email, string provider)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
                return new TokenResponse().WithErrors("Invalid request");

            var token = await _userManager.GenerateTwoFactorTokenAsync(user, provider);

            return new TokenResponse { IsSuccessful = true, Token = token };
        }
        public async Task<TokenResponse> GenerateEmailConfirmationTokenAsync(string email)
        {
            var user = await _userManager.FindByNameAsync(email);

            if (user == null)
                return new TokenResponse().WithErrors("Invalid request");

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return new TokenResponse { IsSuccessful = true, Token = token };
        }

        public async Task<TokenResponse> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new TokenResponse().WithErrors("Invalid request");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return new TokenResponse { IsSuccessful = true, Token = token };
        }
    }
}
