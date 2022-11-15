namespace EventSourcing.Api.Common.EventSourcing
{
    public interface IAggregate : IAggregate<Guid>
    {
    }

    public interface IAggregate<out T>
    {
        T Id { get; }

        int Version { get; }

        void When(IEventState @event);
    }
}
