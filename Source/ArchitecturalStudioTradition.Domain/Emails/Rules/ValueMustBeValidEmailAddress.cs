using ArchitecturalStudioTradition.Domain.SeedWork.Rules;
using System.Text.RegularExpressions;

namespace ArchitecturalStudioTradition.Domain.Emails.Rules
{
    public class ValueMustBeValidEmailAddress : IBusinessRule
    {
        private readonly string _value;

        public ValueMustBeValidEmailAddress(string value)
        {
            _value = value;
        }

        public bool IsValid()
        {
            string email = _value.Trim();
            return email.Length <= 150 && Regex.IsMatch(email, @"^(.+)@(.+)$");
        }

        public string ValidationErrorMessage => "Email is invalid.";
    }
}