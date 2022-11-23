using ArchitecturalStudioTradition.WebApi.Configuration;
using ArchitecturalStudioTradition.FileStorage.Application.Configuration;
using ArchitecturalStudioTradition.FileStorage.Application;

namespace AArchitecturalStudioTradition.FileStorage.WebApi
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApi(this IServiceCollection services)
        {
            services.AddApplication();
            services.AddTransient<IAwsConfiguration, WebApiConfiguration>();
        }
    }
}
