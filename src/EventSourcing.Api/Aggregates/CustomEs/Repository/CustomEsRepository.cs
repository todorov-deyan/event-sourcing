using EventSourcing.Api.Aggregates.CustomEs.Repository.Entities;
using EventSourcing.Api.Common.EventSourcing;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Api.Aggregates.CustomEs.Repository
{
    public class CustomEsRepository<T> : ICustomEsRepository<T> where T : class, IAggregate, new()
    {
        private readonly CustomEsDbContext _dbContext;
        private readonly IEventSerializer _serializer;

        public CustomEsRepository(CustomEsDbContext dbContext)
        {
            _dbContext = dbContext;
            this._serializer = serializer;
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
                    Data = _serializer.Serialize(@event), 
                    EventId = Guid.NewGuid(),
                    EventType = @event.GetType().Name.ToLowerInvariant(),
                };

                stream.Events.Add(ce);
            }

            _dbContext.Add(stream);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<T?> FindReflection(Guid id, CancellationToken cancellationToken)
        {
            var stream =  _dbContext.Streams.Include(x => x.Events).FirstOrDefault(x => x.StreamId == id);
             
            if (stream is null)
                throw new ArgumentException(nameof(CustomStream));

            T aggregate = new T();
            aggregate.Id = stream.StreamId;

            foreach (var @event in stream.Events)
            {
                IEventState  @eventState = _serializer.DeserializeEvent(@event.EventType, @event.Data);
                if(@eventState != null) 
                    aggregate.When(@eventState);
            }

            return aggregate;
        }
    }
}
