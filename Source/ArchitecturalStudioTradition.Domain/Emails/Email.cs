using ArchitecturalStudioTradition.Domain.Emails.Rules;
using ArchitecturalStudioTradition.Domain.SeedWork;

namespace ArchitecturalStudioTradition.Domain.Emails
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string value)
        {
            Validate(new ValueMustBeSet(value));
            Validate(new ValueMustBeValidEmailAddress(value));

            return new Email(value);
        }
    }
}
