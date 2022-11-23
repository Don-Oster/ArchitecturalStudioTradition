using ArchitecturalStudioTradition.Domain.Authors;
using ArchitecturalStudioTradition.Domain.Photos;
using ArchitecturalStudioTradition.Domain.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchitecturalStudioTradition.Database.Mappings
{
    internal class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project", Schema.Default); 
            builder.HasKey(o => o.Id);

            builder.Property(b => b.Id).HasColumnName("Id");

            builder.OwnsOne<Author>("TextAuthor", map =>
            {
                map.Property(p => p.Name).HasColumnName("Name");
                map.Property(p => p.Surname).HasColumnName("Surname");
            });
            builder.OwnsMany<Photo>("Photos", map =>
            {
                map.Property(p => p.Hash).HasColumnName("Hash");
                map.Property(p => p.Filename).HasColumnName("Filename");
                map.Property(p => p.Order).HasColumnName("Order");
                map.UsePropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}
