using ArchitecturalStudioTradition.Domain.SeedWork.Events;

namespace ArchitecturalStudioTradition.Domain.Projects.Events
{
    internal class ProjectCreated : DomainEvent
    {
        public ProjectCreated(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
