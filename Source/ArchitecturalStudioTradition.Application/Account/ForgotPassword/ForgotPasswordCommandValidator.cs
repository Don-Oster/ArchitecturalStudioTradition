using ArchitecturalStudioTradition.Infrastructure.RequestValidation;
using FluentValidation;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    internal class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(RequestValidationMessages.EmailNotEmpty)
                .EmailAddress().WithMessage(RequestValidationMessages.EmailNotValid);
            RuleFor(x => x.ClientUri)
                .NotEmpty().WithMessage(RequestValidationMessages.ClientUriNotEmpty);
        }
    }
}
