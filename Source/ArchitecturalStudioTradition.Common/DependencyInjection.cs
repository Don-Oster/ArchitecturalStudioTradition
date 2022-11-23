using Microsoft.Extensions.DependencyInjection;
using ArchitecturalStudioTradition.Common.Helpers;

namespace ArchitecturalStudioTradition.Common
{
    public static class DependencyInjectionExtensions
    {
        public static void AddCommon(this IServiceCollection services)
        {
            services.AddTransient(typeof(ILazy<>), typeof(LazyFactory<>));
        }
    }
}
