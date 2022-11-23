using ArchitecturalStudioTradition.Domain.Projects;
using Microsoft.EntityFrameworkCore;

namespace ArchitecturalStudioTradition.Database.Repositories
{
    internal class ProjectRepository : IProjectRepository
    {
        private readonly PostgreSqlDbContext _context;

        public ProjectRepository(PostgreSqlDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Project project)
        {
            await _context.AddAsync(project);
        }

        public Task<Project> GetArchitectureProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Project> GetInteriorProjectsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Project>> ListAsync()
        {
            return await _context.Projects.ToListAsync();
        }
    }
}
