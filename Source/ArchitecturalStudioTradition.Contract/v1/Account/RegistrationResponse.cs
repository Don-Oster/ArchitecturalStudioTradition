namespace ArchitecturalStudioTradition.Contract.v1.Account
{
    public class RegistrationResponse : ResponseBase
    {
        public bool IsSuccessful { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public RegistrationResponse WithErrors(params string[] errors)
        {
            AppendErrors(errors);
            return this;
        }
    }
}
