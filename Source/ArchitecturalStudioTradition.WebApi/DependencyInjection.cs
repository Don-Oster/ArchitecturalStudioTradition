using ArchitecturalStudioTradition.WebApi.Configuration;
using ArchitecturalStudioTradition.Application;
using ArchitecturalStudioTradition.Infrastructure.Configuration;

namespace ArchitecturalStudioTradition.WebApi
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApi(this IServiceCollection services, WebApiConfiguration configuration)
        {
            services.AddApplication(configuration);

            services.AddSingleton<IJwtTokenConfiguration>(_ => configuration);
            services.AddSingleton<IGoogleAuthConfiguration>(_ => configuration);
            services.AddSingleton<IFacebookAuthConfiguration>(_ => configuration);
            services.AddSingleton<IEmailConfiguration>(_ => configuration);
        }
    }
}
