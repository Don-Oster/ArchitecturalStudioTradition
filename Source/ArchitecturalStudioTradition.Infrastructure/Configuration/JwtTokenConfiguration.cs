namespace ArchitecturalStudioTradition.Infrastructure.Configuration
{
    public interface IJwtTokenConfiguration
    {
        string JwtSecurityKey { get; }
        string JwtIssuer { get; }
        string JwtAudience { get; }
        int JwtExpiryInMinutes { get; }
    }
}
