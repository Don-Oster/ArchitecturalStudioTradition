using MediatR;

namespace ArchitecturalStudioTradition.CQRS.Interfaces
{
    public interface ICommand : IRequest
    { }

    public interface ICommand<out TResponse> : IRequest<TResponse>
    { }

    public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    { }
}
