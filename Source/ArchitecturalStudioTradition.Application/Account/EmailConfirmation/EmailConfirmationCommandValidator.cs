using ArchitecturalStudioTradition.Infrastructure.RequestValidation;
using FluentValidation;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    internal class EmailConfirmationCommandValidator : AbstractValidator<EmailConfirmationCommand>
    {
        public EmailConfirmationCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(RequestValidationMessages.EmailNotEmpty);
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage(RequestValidationMessages.ClientUriNotEmpty);
        }
    }
}
