using ArchitecturalStudioTradition.Domain.Projects;
using Microsoft.EntityFrameworkCore;

namespace ArchitecturalStudioTradition.Database
{
    public class PostgreSqlDbContext : DbContext
    {
        public PostgreSqlDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlDbContext).Assembly);
        }
    }
}
