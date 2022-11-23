using ArchitecturalStudioTradition.Common.Exceptions;
using ArchitecturalStudioTradition.Infrastructure.Configuration;
using ArchitecturalStudioTradition.Model.Facebook;
using ArchitecturalStudioTradition.Model.UserIdentity;
using Newtonsoft.Json;

namespace ArchitecturalStudioTradition.Infrastructure.Authentication.External
{
    internal class FacebookTokenValidator : ITokenValidator<FacebookUserData>
    {
        private static string AccessTokenUrl(string appId, string appSecret) => $"https://graph.facebook.com/oauth/access_token?client_id={appId}&client_secret={appSecret}&grant_type=client_credentials";
        private static string VerifyTokenUrl(string inputToken, string accessToken) => $"https://graph.facebook.com/debug_token?input_token={inputToken}&access_token={accessToken}";
        private static string UserInfoUrl(string accessToken) => $"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={accessToken}";

        private readonly IFacebookAuthConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public FacebookTokenValidator(IFacebookAuthConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public bool ForProvider(ExternalAuthProvider provider)
        {
            return provider == ExternalAuthProvider.Facebook;
        }

        public async Task<FacebookUserData> ValidateTokenAsync(string token)
        {
            var appAccessTokenResponse = await _httpClient.GetStringAsync(AccessTokenUrl( _configuration.FacebookAppId, _configuration.FacebookAppSecret));
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            
            var userAccessTokenValidationResponse = await _httpClient.GetStringAsync(VerifyTokenUrl(token, appAccessToken.AccessToken));
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (userAccessTokenValidation != null && !userAccessTokenValidation.Data.IsValid)
            {
                throw new InvalidTokenException("Invalid facebook token.");
            }

            var userInfoResponse = await _httpClient.GetStringAsync(UserInfoUrl(token));
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            return userInfo;
        }
    }
}
