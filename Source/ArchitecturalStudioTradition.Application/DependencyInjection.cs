using ArchitecturalStudioTradition.Application.Infrastructure.Behaviors;
using ArchitecturalStudioTradition.Database;
using ArchitecturalStudioTradition.Database.Configuration;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ArchitecturalStudioTradition.Application
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services, IPostgreSqlConfig config)
        {
            services.AddPostgreSQLDatabase(config);
            services.AddInfrastructure();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
