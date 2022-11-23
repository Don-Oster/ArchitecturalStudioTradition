namespace ArchitecturalStudioTradition.Infrastructure.Configuration
{
    public interface IGoogleAuthConfiguration
    {
        string GoogleClientId { get; }
        string GoogleClientSecret { get; }
    }
}
