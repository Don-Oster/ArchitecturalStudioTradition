using ArchitecturalStudioTradition.Domain.SeedWork.Rules;

namespace ArchitecturalStudioTradition.Domain.Authors.Rules
{
    public class SurnameMustBeSet : IBusinessRule
    {
        private readonly string _surname;

        public SurnameMustBeSet(string surname)
        {
            _surname = surname;
        }

        public bool IsValid() => !string.IsNullOrWhiteSpace(_surname);

        public string ValidationErrorMessage => "Author surname cannot be empty.";
    }
}