using ArchitecturalStudioTradition.Common.Exceptions;
using ArchitecturalStudioTradition.Domain.SeedWork.Rules;
using ArchitecturalStudioTradition.Infrastructure.RequestValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.Extensions.DependencyInjection;

namespace ArchitecturalStudioTradition.Infrastructure.ErrorHandling
{
    public static class ProblemDetailsExtensions
    {
        public static void ConfigureProblemDetails(this IServiceCollection services)
        {
            services.AddProblemDetails(x =>
            {
                x.Map<RequestValidationException>(e => new InvalidRequestProblemDetails(e));
                x.Map<BadRequestException>(e => new InvalidRequestProblemDetails(e));
                x.Map<BusinessRuleValidationException>(e => new BusinessRuleValidationProblemDetails(e));
            });
        }
    }
}