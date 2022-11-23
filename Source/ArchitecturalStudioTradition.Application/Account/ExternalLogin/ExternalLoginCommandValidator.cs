using FluentValidation;

namespace ArchitecturalStudioTradition.Application.Account.Login
{
    internal class ExternalLoginCommandValidator : AbstractValidator<ExternalLoginCommand>
    {
        public ExternalLoginCommandValidator()
        {
        }
    }
}
