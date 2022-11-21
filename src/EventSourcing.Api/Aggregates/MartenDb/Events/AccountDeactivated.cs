using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.MartenDb.Events
{
    public class AccountDeactivated : IEventState
    {
        public AccountDeactivated()
        {
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public DateTimeOffset CreatedAt { get; init; }

        public decimal ClosingBalance { get; set; } = 0;

        public string Description { get; set; }

    }
}
