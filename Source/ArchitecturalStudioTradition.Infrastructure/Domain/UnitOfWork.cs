using ArchitecturalStudioTradition.Database;
using ArchitecturalStudioTradition.Domain.SeedWork;
using ArchitecturalStudioTradition.Domain.SeedWork.Rules;
using ArchitecturalStudioTradition.Infrastructure.Domain.EventProcessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArchitecturalStudioTradition.Infrastructure.Domain
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly PostgreSqlDbContext _context;
        private readonly IDomainEventsProcessor _eventsProcessor;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(PostgreSqlDbContext context, IDomainEventsProcessor eventsProcessor, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _eventsProcessor = eventsProcessor;
            _logger = logger;
        }

        public async Task<int> Commit(CancellationToken cancellationToken = default)
        {
            try
            {
                await _eventsProcessor.ProcessDomainEvents();
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException exception)
            {
                _logger.LogError(exception, "Database concurrency exception");
                throw new BusinessRuleValidationException(new ConcurrentRequestsMustBeDetected(true), exception);
            }
        }
    }
}
