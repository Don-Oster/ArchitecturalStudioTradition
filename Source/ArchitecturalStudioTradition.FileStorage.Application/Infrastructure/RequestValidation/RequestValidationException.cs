using FluentValidation.Results;

namespace ArchitecturalStudioTradition.FileStorage.Application.Infrastructure.RequestValidation
{
    [Serializable]
    public class RequestValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public RequestValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public RequestValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
    }
}
