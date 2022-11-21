using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.MartenDb.Events
{
    public class AccountActivated : IEventState
    {
        public AccountActivated()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset CreatedAt { get; init; }

        public decimal Balance { get; set; }

        public string Description { get; set; }
    }
}
