using ArchitecturalStudioTradition.Infrastructure.Configuration;
using ArchitecturalStudioTradition.Model.UserIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ArchitecturalStudioTradition.Application.Authentication
{
    public interface IAuthTokenGenerator
    {
        public Task<string> GenerateJwtTokenAsync(User user);
    }

    internal class AuthTokenGenerator : IAuthTokenGenerator
    {

        private readonly IJwtTokenConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public AuthTokenGenerator(UserManager<User> userManager, IJwtTokenConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtSecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration.JwtIssuer,
                audience: _configuration.JwtAudience,
                claims: await GetClaims(user),
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration.JwtExpiryInMinutes)),
                signingCredentials: signingCredentials
           );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
