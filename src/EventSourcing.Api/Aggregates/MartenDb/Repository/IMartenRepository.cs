using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.MartenDb.Repository
{
    public interface IMartenRepository<T> where T : class, IAggregate
    {
        Task<T?> Find(Guid id, CancellationToken cancellationToken);

        Task Add(T aggregate, IList<IEventState> events, CancellationToken cancellationToken = default);

        Task Update(Guid id, IList<IEventState> events, CancellationToken cancellationToken = default);

        Task<List<T>> FindAll(Guid id, CancellationToken cancellationToken = default);
    }
}
