namespace ArchitecturalStudioTradition.Domain.SeedWork.Rules
{
    public class ConcurrentRequestsMustBeDetected : IBusinessRule
    {
        private readonly bool _concurrentRequestDetected;

        public ConcurrentRequestsMustBeDetected(bool concurrentRequestDetected)
        {
            _concurrentRequestDetected = concurrentRequestDetected;
        }

        public bool IsValid()
        {
            return !_concurrentRequestDetected;
        }

        public string ValidationErrorMessage =>
            "Another request has modified the same entity before the transaction has finished.";
    }
}