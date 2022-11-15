using MediatR;

namespace EventSourcing.Api.Common.CQRS
{
    public interface IQueryRequest<out TResponse> : IRequest<TResponse>
    {
    }
}
