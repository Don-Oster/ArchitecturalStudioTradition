namespace ArchitecturalStudioTradition.Database.Configuration
{
    public interface IPostgreSqlConfig
    {
        string DbHost { get; }
        int DbPort { get; }
        string DbName { get; }
        string DbUsername { get; }
        string DbPassword { get; }
    }
}