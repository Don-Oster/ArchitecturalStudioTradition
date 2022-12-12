using ArchitecturalStudioTradition.Common.Exceptions;
using ArchitecturalStudioTradition.Domain.SeedWork.Rules;
using ArchitecturalStudioTradition.Infrastructure.RequestValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;

namespace ArchitecturalStudioTradition.Infrastructure.ErrorHandling
{
    public static class ProblemDetailsExtensions
    {
        public static void ConfigureProblemDetails(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services.AddProblemDetails(options =>
            {
                options.MapToStatusCode<RequestValidationException>(StatusCodes.Status400BadRequest);
                options.MapToStatusCode<BadRequestException>(StatusCodes.Status400BadRequest);
                options.MapToStatusCode<BusinessRuleValidationException>(StatusCodes.Status409Conflict);
            });
        }
    }
}