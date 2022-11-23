using ArchitecturalStudioTradition.Common.Exceptions;
using ArchitecturalStudioTradition.Infrastructure.RequestValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArchitecturalStudioTradition.Infrastructure.ErrorHandling
{
    public class InvalidRequestProblemDetails : ProblemDetails
    {
        public InvalidRequestProblemDetails(RequestValidationException exception)
        {
            Title = "Invalid request";
            Status = StatusCodes.Status400BadRequest;
            Detail = exception.Message;
            Type = "https://httpstatuses.com/400";
        }

        public InvalidRequestProblemDetails(BadRequestException exception)
        {
            Title = "Invalid request";
            Status = StatusCodes.Status400BadRequest;
            Detail = exception.Message;
            Type = "https://httpstatuses.com/400";
        }
    }
}