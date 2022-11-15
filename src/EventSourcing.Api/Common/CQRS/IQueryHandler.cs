using MediatR;

namespace EventSourcing.Api.Common.CQRS
{
    public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IQueryRequest<TResponse>
    {
    }

}
