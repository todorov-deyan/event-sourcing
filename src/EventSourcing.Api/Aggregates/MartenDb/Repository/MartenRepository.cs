using Marten;

using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.MartenDb.Repository
{
    public class MartenRepository<T> : IMartenRepository<T> where T : class, IAggregate
    {
        private readonly IDocumentSession _documentSession;

        public MartenRepository(IDocumentSession documentSession)
        {
            _documentSession = documentSession ?? throw new ArgumentNullException(nameof(documentSession));
        }

        public Task Add(T aggregate, IList<IEventState> events, CancellationToken cancellationToken = default)
        {
            _documentSession.Events.StartStream<T>(aggregate.Id, events);

            return _documentSession.SaveChangesAsync(cancellationToken);
        }

        public Task<T?> Find(Guid id, CancellationToken cancellationToken = default)
        {
            return _documentSession.Events.AggregateStreamAsync<T>(id, token: cancellationToken);
        }

        public Task Update(Guid id, IList<IEventState> events, CancellationToken cancellationToken = default)
        {
            _documentSession.Events.Append(id, events);

            return _documentSession.SaveChangesAsync(cancellationToken);
        }
    }
}
