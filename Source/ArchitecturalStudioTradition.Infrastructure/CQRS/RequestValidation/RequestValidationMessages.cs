namespace ArchitecturalStudioTradition.Infrastructure.RequestValidation
{
    public static class RequestValidationMessages
    {
        public const string EmailNotEmpty = "Email must be provided.";
        public const string EmailNotValid = "Email has incorrect value.";
        public const string TokenNotEmpty = "Token must be provided.";
        public const string ProviderNotEmpty = "Provider must be provided.";
        public const string ClientUriNotEmpty = "ClientUri must be provided.";
        public const string PasswordNotEmpty = "Password must be provided.";
        public const string PasswordIncorrectLength = "Password has incorrect length.";
        public const string PasswordIncorrectUppercaseLetter = "Password has incorrect uppercase letter.";
        public const string PasswordIncorrectLowercaseLetter = "Password has incorrect lowercase letter.";
        public const string PasswordIncorrectDigit = "Password has incorrect digit.";
        public const string PasswordIncorrectSpecialCharacter = "Password has incorrect special character.";
    }
}
