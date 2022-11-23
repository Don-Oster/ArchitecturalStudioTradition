using ArchitecturalStudioTradition.Infrastructure.RequestValidation;
using FluentValidation;

namespace ArchitecturalStudioTradition.Application.Account.Register
{
    internal class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(RequestValidationMessages.EmailNotEmpty)
                .EmailAddress().WithMessage(RequestValidationMessages.EmailNotValid);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(RequestValidationMessages.PasswordNotEmpty)
                .Password(10);
            RuleFor(x => x.ClientUri)
                .NotEmpty().WithMessage(RequestValidationMessages.ClientUriNotEmpty);
        }
    }
}
