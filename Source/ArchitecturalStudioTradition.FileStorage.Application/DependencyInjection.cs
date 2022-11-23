using ArchitecturalStudioTradition.Application.Infrastructure.Behaviors;
using ArchitecturalStudioTradition.FileStorage.Application.Files;
using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws;
using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.Aws.Helpers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PgcrDashboards.Admin.Web.Infrastructure;
using System.Reflection;

namespace ArchitecturalStudioTradition.FileStorage.Application
{
    public static class DependencyInjectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<IS3RequestBuilder, S3RequestBuilder>();
            services.AddTransient<IS3FileRepository, S3FileRepository>();
            services.AddTransient<IS3Client, S3Client>();
            services.AddTransient<ICloudFrontUrlProvider, CloudFrontUrlProvider>();
            services.AddTransient<IFileRepository, FileRepository>();
        }
    }
}
