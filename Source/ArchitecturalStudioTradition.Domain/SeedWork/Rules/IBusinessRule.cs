namespace ArchitecturalStudioTradition.Domain.SeedWork.Rules
{
    public interface IBusinessRule
    {
        string ValidationErrorMessage { get; }
        bool IsValid();
    }
}