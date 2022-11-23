using ArchitecturalStudioTradition.Domain.Projects;
using ArchitecturalStudioTradition.Model.UserIdentity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArchitecturalStudioTradition.Database
{
    public class PostgreSqlDbContext : IdentityDbContext<User>
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
