using ArchitecturalStudioTradition.CQRS.Interfaces;
using MediatR;

namespace ArchitecturalStudioTradition.CQRS
{
    public interface ICommandBus
    {
        Task<T> SendAsync<T>(ICommand<T> command);
        Task PublishAsync<T>(ICommand<T> command);
        Task PublishAsync<T>(T notification) where T : INotification;
    }

    internal class CommandBus : ICommandBus
    {
        private readonly IMediator _mediator;

        public CommandBus(IMediator mediator) => _mediator = mediator;

        public Task<T> SendAsync<T>(ICommand<T> command) => _mediator.Send(command);
        public Task PublishAsync<T>(ICommand<T> command) => _mediator.Publish(command);
        public Task PublishAsync<T>(T notification) where T : INotification
        {
            return _mediator.Publish(notification);
        }
    }
}