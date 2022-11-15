using MediatR;

namespace EventSourcing.Api.Common.CQRS
{
    public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : ICommandRequest<TResponse>
    {
    }
}
