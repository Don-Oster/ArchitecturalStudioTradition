using ArchitecturalStudioTradition.Infrastructure.RequestValidation;
using FluentValidation;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    internal class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(RequestValidationMessages.EmailNotEmpty)
                .EmailAddress().WithMessage(RequestValidationMessages.EmailNotValid);
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(RequestValidationMessages.PasswordNotEmpty)
                .Password(10);
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage(RequestValidationMessages.TokenNotEmpty);
        }
    }
}
