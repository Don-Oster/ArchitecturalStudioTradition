using ArchitecturalStudioTradition.Identity.Application.Database;
using ArchitecturalStudioTradition.Model.UserIdentity;
using Microsoft.AspNetCore.Identity;

namespace ArchitecturalStudioTradition.Identity.WebApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddIdentityServer(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddIdentity<User, IdentityRole>(opt =>
         {
             opt.Password.RequiredLength = 8;
             opt.Password.RequireDigit = false;

             opt.User.RequireUniqueEmail = true;

             opt.Lockout.AllowedForNewUsers = true;
             opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
             opt.Lockout.MaxFailedAccessAttempts = 3;
         })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        var identityServerBuilder = services.AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
        })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<User>();
            //.AddResourceOwnerValidator<UserValidator>();

        if (env.IsDevelopment())
        {
            identityServerBuilder.AddDeveloperSigningCredential();
        }

        return services;
    }
}
