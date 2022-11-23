using ArchitecturalStudioTradition.CQRS;
using ArchitecturalStudioTradition.Domain.SeedWork.Events;

namespace ArchitecturalStudioTradition.Infrastructure.Domain.EventProcessing
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEvents(IReadOnlyCollection<IDomainEvent> domainEvents);
    }

    internal class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly ICommandBus _commandBus;

        public DomainEventsDispatcher(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        public async Task DispatchEvents(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
                await _commandBus.PublishAsync(domainEvent);
        }
    }
}
