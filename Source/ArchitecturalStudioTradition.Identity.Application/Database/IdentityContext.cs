using ArchitecturalStudioTradition.Model.UserIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArchitecturalStudioTradition.Identity.Application.Database;

public sealed class IdentityContext : IdentityDbContext<User, 
    IdentityRole<long>, 
    long,
    IdentityUserClaim<long>,
    IdentityUserRole<long>, 
    IdentityUserLogin<long>, 
    IdentityRoleClaim<long>, 
    IdentityUserToken<long>>
{

    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}