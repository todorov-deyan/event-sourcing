using EventSourcing.Api.Aggregates.CustomEs.Repository.Entities;
using EventSourcing.Api.Common.EventSourcing;
using EventSourcing.Api.Common.Extensions;

namespace EventSourcing.Api.Aggregates.CustomEs.Repository
{
    public class CustomEsRepository<T> : ICustomEsRepository<T> where T : class, IAggregate
    {
        private readonly CustomEsDbContext _dbContext;
        private readonly IEventSerializer _eventSerializer;

        public CustomEsRepository(CustomEsDbContext dbContext, IEventSerializer eventSerializer)
        {
            _dbContext = dbContext;
            _eventSerializer = eventSerializer;
        }

        public async Task Update(Guid id, IList<IEventState> events, CancellationToken cancellationToken = default)
        {
            var stream = await  _dbContext.Streams.FindAsync(id);

            if (stream == null)
                throw new NullReferenceException();

            foreach (var @event in events)
            {
                var ce = new CustomEvent
                {
                    StreamId = stream.StreamId,
                    CreatedAt = DateTime.UtcNow,
                    Data = _eventSerializer.ToJSON(@event),
                    EventId = Guid.NewGuid(),
                    EventType = @event.GetType().Name.ToLowerInvariant()
                };

                _dbContext.Events.Add(ce);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Add(T aggregate, IList<IEventState> events, CancellationToken cancellationToken = default)
        {
            var stream = new CustomStream
            {
                StreamId = aggregate.Id,
                Type = aggregate.GetType().Name.ToLowerInvariant(),
                CreatedAt = DateTime.UtcNow,
            };

            foreach (var @event in events)
            {
                var ce = new CustomEvent
                {
                    StreamId = stream.StreamId,
                    CreatedAt = DateTime.UtcNow,
                    Data = _eventSerializer.ToJSON(@event),
                    EventId = Guid.NewGuid(),
                    EventType = @event.GetType().Name.ToLowerInvariant(),
                };

                stream.Events.Add(ce);
            }

            _dbContext.Add(stream);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<T?> Find(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
