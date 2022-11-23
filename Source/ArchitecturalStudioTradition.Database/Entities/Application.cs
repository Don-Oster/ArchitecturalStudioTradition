using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArchitecturalStudioTradition.Database.Entities
{
    [Table(nameof(Application))]
    public class Application : Entity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Editor { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
