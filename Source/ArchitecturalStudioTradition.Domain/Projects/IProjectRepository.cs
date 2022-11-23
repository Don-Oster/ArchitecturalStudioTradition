namespace ArchitecturalStudioTradition.Domain.Projects
{
    public interface IProjectRepository
    {
        Task AddAsync(Project element);
        Task<Project> GetInteriorProjectsAsync();
        Task<Project> GetArchitectureProjectsAsync();
        Task<List<Project>> ListAsync();
    }
}
