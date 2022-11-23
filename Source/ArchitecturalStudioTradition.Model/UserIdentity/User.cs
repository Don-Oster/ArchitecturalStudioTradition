using Microsoft.AspNetCore.Identity;

namespace ArchitecturalStudioTradition.Model.UserIdentity
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}