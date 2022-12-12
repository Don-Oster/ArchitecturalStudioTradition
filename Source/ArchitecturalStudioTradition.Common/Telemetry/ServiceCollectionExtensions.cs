using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ArchitecturalStudioTradition.Common.Telemetry
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOpenTelemetry(this IServiceCollection services)
        {
            services
                .AddOpenTelemetryTracing(builder => builder
                .AddGrpcClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddNpgsql()
                .SetResourceBuilder(ResourceBuilder.CreateDefault())
                .AddJaegerExporter());

            return services;
        }
    }
}