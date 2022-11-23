namespace ArchitecturalStudioTradition.Database.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public DateTime CreatedAt { get; set; }
    }
}