namespace EventSourcing.Api.Common.EventSourcing
{
    public interface IAggregate : IAggregate<Guid>
    {
    }

    public interface IAggregate<T>
    {
        T Id { get; set; }

        int Version { get; }

        void When(IEventState @event);
    }
}
