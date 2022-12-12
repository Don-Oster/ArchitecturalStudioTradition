using Microsoft.AspNetCore.Identity;

namespace ArchitecturalStudioTradition.Model.UserIdentity
{
    public class User : IdentityUser<long>
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
    }
}