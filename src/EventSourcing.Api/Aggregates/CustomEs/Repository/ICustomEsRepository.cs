using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.CustomEs.Repository
{
    public interface ICustomEsRepository<T> where T : class, IAggregate
    {
        Task Update(Guid id, IList<IEventState> events, CancellationToken cancellationToken = default);

        Task Add(T aggregate, IList<IEventState> events, CancellationToken cancellationToken = default);

        Task<T?> Find(Guid id, CancellationToken cancellationToken);
    }
}
