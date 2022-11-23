using ArchitecturalStudioTradition.Domain.SeedWork.Rules;

namespace ArchitecturalStudioTradition.Domain.Emails.Rules
{
    public class ValueMustBeSet : IBusinessRule
    {
        private readonly string _name;

        public ValueMustBeSet(string name)
        {
            _name = name;
        }

        public bool IsValid() => !string.IsNullOrWhiteSpace(_name);

        public string ValidationErrorMessage => "Email cannot be empty.";
    }
}