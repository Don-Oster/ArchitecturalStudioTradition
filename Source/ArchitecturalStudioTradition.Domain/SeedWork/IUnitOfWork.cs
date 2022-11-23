namespace ArchitecturalStudioTradition.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task<int> Commit(CancellationToken cancellationToken = default);
    }
}