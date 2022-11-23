using ArchitecturalStudioTradition.Domain.SeedWork.Events;
using ArchitecturalStudioTradition.Domain.SeedWork.Rules;

namespace ArchitecturalStudioTradition.Domain.SeedWork
{
    public abstract class Entity
    {
        protected List<IDomainEvent> _domainEvents;

        public virtual long Id { get; protected set; }

        protected Entity()
        {
            _domainEvents = new List<IDomainEvent>();
        }

        public bool HasDomainEvents()
        {
            return _domainEvents.Any();
        }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.AsReadOnly();
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        protected static void Validate(IBusinessRule rule)
        {
            if (!rule.IsValid())
                throw new BusinessRuleValidationException(rule);
        }

        public override bool Equals(object obj)
        {
            if (obj is not Entity other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}