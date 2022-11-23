namespace ArchitecturalStudioTradition.Contract.v1.Account
{
    public class TokenResponse : ResponseBase
    {
        public bool IsSuccessful { get; set; }
        public string? Token { get; set; }

        public TokenResponse WithErrors(params string[] errors)
        {
            AppendErrors(errors);
            return this;
        }
    }
}
