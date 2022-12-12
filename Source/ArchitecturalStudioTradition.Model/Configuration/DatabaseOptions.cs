using System.ComponentModel.DataAnnotations;

namespace ArchitecturalStudioTradition.Model.Configuration
{
    public record DatabaseOptions
    {
        [Required]
        public string DefaultConnection { get; init; }
    }
}