using ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.RequestValidation;
using FluentValidation;
using MediatR;

namespace ArchitecturalStudioTradition.Application.Infrastructure.Behaviors
{
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationErrors = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (validationErrors.Any())
            {
                throw new RequestValidationException(validationErrors);
            }

            return await next();
        }
    }
}
