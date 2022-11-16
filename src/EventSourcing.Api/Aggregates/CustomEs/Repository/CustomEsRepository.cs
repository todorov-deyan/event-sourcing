using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.CustomEs.Repository
{
    public class CustomEsRepository<T> : ICustomEsRepository<T> where T : class, IAggregate
    {
        public Task Update(Guid id, IList<IEventState> events, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Add(T aggregate, IList<IEventState> events, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
