namespace ArchitecturalStudioTradition.Contract.v1.Account
{
    public class AuthenticationResponse : ResponseBase
    {
        public bool IsSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
        public bool Is2StepVerificationRequired { get; set; }
        public bool IsLockedOut { get; set; }
        public string? Provider { get; set; }

        public AuthenticationResponse WithErrors(params string[] errors)
        {
            AppendErrors(errors);
            return this;
        }
    }
}
