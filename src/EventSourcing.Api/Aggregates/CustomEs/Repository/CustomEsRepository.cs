using EventSourcing.Api.Aggregates.CustomEs.Repository.Entities;
using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.CustomEs.Repository
{
    public class CustomEsRepository<T> : ICustomEsRepository<T> where T : class, IAggregate
    {
        private readonly CustomEsDbContext _dbContext;

        public CustomEsRepository(CustomEsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Update(Guid id, IList<IEventState> events, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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
                    Data = "{}", // TODO
                    EventId = Guid.NewGuid(),
                    EventType = @event.GetType().Name.ToLowerInvariant()
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
