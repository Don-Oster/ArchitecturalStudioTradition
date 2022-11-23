using ArchitecturalStudioTradition.Common.Exceptions;
using ArchitecturalStudioTradition.Infrastructure.Configuration;
using ArchitecturalStudioTradition.Model.UserIdentity;
using Google.Apis.Auth;

namespace ArchitecturalStudioTradition.Infrastructure.Authentication.External
{
    using GoogleWebSignaturePayload = GoogleJsonWebSignature.Payload;

    internal class GoogleTokenValidator : ITokenValidator<GoogleWebSignaturePayload>
    {
        private readonly IGoogleAuthConfiguration _configuration;

        public GoogleTokenValidator(IGoogleAuthConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ForProvider(ExternalAuthProvider provider)
        {
            return provider == ExternalAuthProvider.Google;
        }

        public async Task<GoogleWebSignaturePayload> ValidateTokenAsync(string token)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { _configuration.GoogleClientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
                return payload;
            }
            catch (InvalidJwtException)
            {
                throw new InvalidTokenException("Invalid google token.");
            }
        }
    }
}
