using MediatR;

namespace EventSourcing.Api.Common.CQRS
{
    public interface ICommandRequest <out TResponse> : IRequest<TResponse>
    {
    }
}
