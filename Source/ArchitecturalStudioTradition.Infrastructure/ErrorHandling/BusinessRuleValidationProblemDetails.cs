using ArchitecturalStudioTradition.Domain.SeedWork.Rules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArchitecturalStudioTradition.Infrastructure.ErrorHandling
{
    public class BusinessRuleValidationProblemDetails : ProblemDetails
    {
        public BusinessRuleValidationProblemDetails(BusinessRuleValidationException exception)
        {
            Title = "Business rule validation error";
            Status = StatusCodes.Status409Conflict;
            Detail = exception.Message;
            Type = "https://httpstatuses.com/409";
        }
    }
}