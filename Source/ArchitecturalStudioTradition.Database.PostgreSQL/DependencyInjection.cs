using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ArchitecturalStudioTradition.Database.Configuration;

namespace ArchitecturalStudioTradition.Database
{
    public static class DependencyInjectionExtensions
    {
        public static void AddPostgreSQLDatabase(this IServiceCollection services, IPostgreSqlConfig config, IdentityBuilder? identityBuilder = null)
        {
            services.AddDbContext<PostgreSqlDbContext>(options => options.UseNpgsql(new PostgreSqlDbConfiguration(config).Connection()));

            if (identityBuilder != null)
            {
                identityBuilder.AddEntityFrameworkStores<PostgreSqlDbContext>();
            }
        }
    }
}
