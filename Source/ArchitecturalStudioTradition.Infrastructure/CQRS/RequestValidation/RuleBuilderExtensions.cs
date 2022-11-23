using FluentValidation;

namespace ArchitecturalStudioTradition.Infrastructure.RequestValidation
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string?> ruleBuilder, int minimumLength = 12)
        {
            var options = ruleBuilder
                .MinimumLength(minimumLength).WithMessage(RequestValidationMessages.PasswordIncorrectLength)
                .Matches("[A-Z]").WithMessage(RequestValidationMessages.PasswordIncorrectUppercaseLetter)
                .Matches("[a-z]").WithMessage(RequestValidationMessages.PasswordIncorrectLowercaseLetter)
                .Matches("[0-9]").WithMessage(RequestValidationMessages.PasswordIncorrectDigit)
                .Matches("[^a-zA-Z0-9]").WithMessage(RequestValidationMessages.PasswordIncorrectSpecialCharacter);
            return options;
        }
    }
}
