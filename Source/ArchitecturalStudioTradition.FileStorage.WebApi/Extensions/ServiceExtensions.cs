using Amazon.Extensions.NETCore.Setup;
using Amazon.SecurityToken;

namespace ArchitecturalStudioTradition.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        public static void AddAws(this IServiceCollection services, AWSOptions options)
        {
            services.AddDefaultAWSOptions(options);
            services.AddAWSService<IAmazonSecurityTokenService>();
        }
    }
}
