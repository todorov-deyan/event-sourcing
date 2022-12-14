using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.MartenDb.Events
{
    public class AccountCreated : IEventState
    {
        public AccountCreated()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public string Owner { get; set; }
        public decimal Balance { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreatedAt { get; init; }
    }
}
