using ArchitecturalStudioTradition.Domain.SeedWork.Rules;

namespace ArchitecturalStudioTradition.Domain.Photos.Rules
{
    public class HashMustBeDefined : IBusinessRule
    {
        private readonly string _hash;

        public HashMustBeDefined(string hash)
        {
            _hash = hash;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(_hash);
        }

        public string ValidationErrorMessage => "Hash must be provided for Image.";
    }
}