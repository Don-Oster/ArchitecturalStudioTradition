using MediatR;

namespace ArchitecturalStudioTradition.Domain.SeedWork.Events
{
    public interface IDomainEvent : INotification
    {
        DateTime Occured { get; }
    }

    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent()
        {
            Occured = Clock.Now;
        }

        public DateTime Occured { get; }
    }
}