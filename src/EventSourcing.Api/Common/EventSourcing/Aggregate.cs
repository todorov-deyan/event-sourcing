namespace EventSourcing.Api.Common.EventSourcing
{
    public abstract class Aggregate : IAggregate
    {
        public Guid Id { get; protected set; }

        public int Version { get; }

        public abstract void When(IEventState @event);
    }
}
