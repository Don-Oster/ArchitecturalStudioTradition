using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ArchitecturalStudioTradition.CQRS
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCQRS(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddMediatR(assemblies);
            services.AddTransient<ICommandBus, CommandBus>();
            services.AddTransient<IQueryProcessor, QueryProcessor>();
            return services;
        }
    }
}
