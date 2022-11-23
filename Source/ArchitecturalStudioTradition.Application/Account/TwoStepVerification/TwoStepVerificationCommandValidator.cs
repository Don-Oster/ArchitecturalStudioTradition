using ArchitecturalStudioTradition.Infrastructure.RequestValidation;
using FluentValidation;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    internal class TwoStepVerificationCommandValidator : AbstractValidator<TwoStepVerificationCommand>
    {
        public TwoStepVerificationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(RequestValidationMessages.EmailNotEmpty)
                .EmailAddress().WithMessage(RequestValidationMessages.EmailNotValid);
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage(RequestValidationMessages.TokenNotEmpty);
            RuleFor(x => x.Provider)
                .NotEmpty().WithMessage(RequestValidationMessages.ProviderNotEmpty);
        }
    }
}
