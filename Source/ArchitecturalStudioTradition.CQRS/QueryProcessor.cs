using ArchitecturalStudioTradition.CQRS.Interfaces;
using MediatR;

namespace ArchitecturalStudioTradition.CQRS
{
    public interface IQueryProcessor
    {
        Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query);
    }

    internal class QueryProcessor : IQueryProcessor
    {
        private readonly IMediator _mediator;

        public QueryProcessor(IMediator mediator) => _mediator = mediator;

        public Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query) => _mediator.Send(query);
    }
}