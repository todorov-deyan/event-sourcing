﻿using EventSourcing.Api.Aggregates.CustomEs.Repository.Entities;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Common.EventSourcing;
using EventSourcing.Api.Common.Extensions;
using Microsoft.EntityFrameworkCore;


namespace EventSourcing.Api.Aggregates.CustomEs.Repository
{
    public class CustomEsRepository<T> : ICustomEsRepository<T> where T : class, IAggregate, new()
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

        public async Task<T?> Find(Guid id, CancellationToken cancellationToken)
        {
            var stream =  _dbContext.Streams.Include(x=>x.Events).FirstOrDefault(x => x.StreamId == id);
          
            if (stream == null)
                throw new ArgumentNullException(nameof(CustomStream));

            T aggregate = new T();
            aggregate.Id = stream.StreamId;

            foreach (var @event in stream.Events)
            {
                IEventState @eventState = null;

                switch (@event.EventType)
                {
                    case "accountcreated":
                        @eventState = _eventSerializer.FromJSON<AccountCreated>(@event.Data);
                        break;
                    case "accountactivated":
                        @eventState = _eventSerializer.FromJSON<AccountActivated>(@event.Data);
                        break;
                    case "accountdeactivated":
                        @eventState = _eventSerializer.FromJSON<AccountDeactivated>(@event.Data);
                        break;

                    default:
                        break;
                }

                if (@eventState != null)
                    aggregate.When(@eventState);
            }

            return aggregate;
        }
    }
}
