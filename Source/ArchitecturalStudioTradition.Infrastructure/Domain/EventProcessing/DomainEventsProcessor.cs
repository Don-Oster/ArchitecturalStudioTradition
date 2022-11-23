using ArchitecturalStudioTradition.Database;
using ArchitecturalStudioTradition.Domain.SeedWork;

namespace ArchitecturalStudioTradition.Infrastructure.Domain.EventProcessing
{
    public interface IDomainEventsProcessor
    {
        Task ProcessDomainEvents();
    }

    internal class DomainEventsProcessor : IDomainEventsProcessor
    {
        private readonly PostgreSqlDbContext _context;
        private readonly IDomainEventsDispatcher _eventsDispatcher;

        public DomainEventsProcessor(PostgreSqlDbContext context, IDomainEventsDispatcher eventsDispatcher)
        {
            _context = context;
            _eventsDispatcher = eventsDispatcher;
        }

        public async Task ProcessDomainEvents()
        {
            while (EventsToProcessExist())
            {
                var entitiesWithDomainEvents = _context.ChangeTracker
                    .Entries<Entity>()
                    .Where(x => x.Entity.HasDomainEvents())
                    .ToList();

                var domainEvents = entitiesWithDomainEvents
                    .SelectMany(x => x.Entity.GetDomainEvents())
                    .ToList();

                entitiesWithDomainEvents.ForEach(x => x.Entity.ClearDomainEvents());

                await _eventsDispatcher.DispatchEvents(domainEvents);
            }
        }

        private bool EventsToProcessExist()
        {
            return _context.ChangeTracker.Entries<Entity>().Any(x => x.Entity.HasDomainEvents());
        }
    }
}
