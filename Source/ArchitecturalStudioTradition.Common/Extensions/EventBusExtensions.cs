using ArchitecturalStudioTradition.Model.Configuration;
using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;

namespace ArchitecturalStudioTradition.Common.Extensions;

public static class EventBusExtensions
{
    public static IServiceCollection AddDotNetCoreCap(this IServiceCollection services)
    {
        var rabbitMqOptions = services.GetOptions<RabbitMQOptions>("RabbitMq");
        var sqlOptions = services.GetOptions<DatabaseOptions>("ConnectionStrings");

        services.AddCap(x =>
        {
            x.UseRabbitMQ(o =>
            {
                o.HostName = rabbitMqOptions.HostName;
                o.UserName = rabbitMqOptions.UserName;
                o.Password = rabbitMqOptions.Password;
            });
            x.UsePostgreSql(sqlOptions.DefaultConnection);
            x.UseDashboard();
            x.FailedRetryCount = 5;
        });

        services.Scan(s => s
                .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(c => c.AssignableTo(typeof(ICapSubscribe)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        return services;
    }
}