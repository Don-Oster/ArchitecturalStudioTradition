using ArchitecturalStudioTradition.Infrastructure.Domain.EventProcessing;
using ArchitecturalStudioTradition.Infrastructure.Domain;
using ArchitecturalStudioTradition.Infrastructure.EmailManagement;
using Microsoft.Extensions.DependencyInjection;
using ArchitecturalStudioTradition.Domain.SeedWork;
using ArchitecturalStudioTradition.Infrastructure.Authentication;
using ArchitecturalStudioTradition.Application.Authentication;

namespace ArchitecturalStudioTradition.Application
{
    public static class DependencyInjectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IIdentityProvider, IdentityProvider>();
            services.AddTransient<IAuthTokenGenerator, AuthTokenGenerator>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISmtpClientFactory, SmtpClientFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDomainEventsProcessor, DomainEventsProcessor>();
            services.AddTransient<IDomainEventsDispatcher, DomainEventsDispatcher>();
        }
    }
}
