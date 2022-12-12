using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ArchitecturalStudioTradition.Common.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            var jwtOptions = services.GetOptions<JwtBearerOptions>("Jwt");

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = jwtOptions.Authority;
                options.Audience = jwtOptions.Audience;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true
                };
            });

            return services;
        }
    }
}