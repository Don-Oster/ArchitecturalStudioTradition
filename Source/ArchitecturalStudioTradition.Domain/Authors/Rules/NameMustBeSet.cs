using ArchitecturalStudioTradition.Domain.SeedWork.Rules;

namespace ArchitecturalStudioTradition.Domain.Authors.Rules
{
    public class NameMustBeSet : IBusinessRule
    {
        private readonly string _name;

        public NameMustBeSet(string name)
        {
            _name = name;
        }

        public bool IsValid() => !string.IsNullOrWhiteSpace(_name);

        public string ValidationErrorMessage => "Author name cannot be empty.";
    }
}