using System.Runtime.Serialization;

namespace ArchitecturalStudioTradition.Domain.SeedWork.Rules
{
    [Serializable]
    public class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(IBusinessRule rule) : base(rule.ValidationErrorMessage)
        {
        }

        public BusinessRuleValidationException(IBusinessRule rule, Exception innerException) : base(
            rule.ValidationErrorMessage, innerException)
        {
        }
        
        protected BusinessRuleValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}