using ArchitecturalStudioTradition.WebApi.GraphQl;
using HotChocolate.Types.Pagination;

namespace ArchitecturalStudioTradition.WebApi.Extensions;

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

    public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {

        });

    public static void AddApplicationGraphQL(this IServiceCollection services) =>
        services
        .AddGraphQLServer()
        .AddAuthorization()
        .AddQueryType<Query>()
        .AddMutationType<Mutation>()
        .AddSubscriptionType<Subscription>()
        .AddProjections()
        .AddFiltering()
        .AddSorting()
        .SetPagingOptions(new PagingOptions
        {
            MaxPageSize = 1000,
            DefaultPageSize = 100
        })
        .UseAutomaticPersistedQueryPipeline()
        .AddInMemoryQueryStorage();
        //.AddHttpRequestInterceptor<HttpRequestInterceptor>();
}
