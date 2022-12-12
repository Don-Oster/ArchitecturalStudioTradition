using Microsoft.Extensions.DependencyInjection;
using static ArchitecturalStudioTradition.FileStorage.Client.FileStorage;

namespace ArchitecturalStudioTradition.FileStorage.Client
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFileStorageGrpcClient(this IServiceCollection services, string address)
        {
            services.AddGrpcClient<FileStorageClient>(o =>
            {
                o.Address = new Uri(address);
            });

            return services;
        }
    }
}