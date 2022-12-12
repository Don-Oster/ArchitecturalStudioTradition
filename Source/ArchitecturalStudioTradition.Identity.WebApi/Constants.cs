namespace ArchitecturalStudioTradition.Identity.WebApi;

public static class Constants
{
    public static class Role
    {
        public const string Admin = "admin";
        public const string Dealer = "dealer";
        public const string User = "user";
    }

    public static class StandardScopes
    {
        public const string Roles = "roles";
        public const string FileStorageApi = "file-storage-api";
        public const string IdentityApi = "identity-api";
    }
}
