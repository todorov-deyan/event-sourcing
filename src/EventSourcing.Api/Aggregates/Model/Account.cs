using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.Model
{
    public class Account : Aggregate
    {
        public string Owner { get; set; }

        public decimal Balance { get; set; }

        public AccountStatus Status { get; set; }

        public Account()
        {
            Id = Guid.NewGuid();
        }

        public void Apply(AccountCreated @event)
        {
            Owner = @event.Owner;
            Balance = @event.Balance;
            Status = AccountStatus.Created;
        }

        public void Apply(AccountActivated @event)
        {
            Balance = @event.Balance;
            Status = AccountStatus.Activated;
        }

        public void Apply(AccountDeactivated @event)
        {
            Balance = @event.ClosingBalance;
            Status = AccountStatus.Deactivated;
        }

        public override void When(IEventState @event)
        {
            switch (@event)
            {
                case AccountCreated accountCreated:
                    Apply(accountCreated);
                    break;
                case AccountActivated accountActivated:
                    Apply(accountActivated);
                    break;
                case AccountDeactivated accountDeactivated:
                    Apply(accountDeactivated);
                    break;
            }
        }
    }
}
