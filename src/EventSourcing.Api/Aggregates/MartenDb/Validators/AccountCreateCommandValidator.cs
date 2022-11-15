using EventSourcing.Api.Aggregates.MartenDb.Commands;

using FluentValidation;

namespace EventSourcing.Api.Aggregates.MartenDb.Validators
{
    public class AccountCreateCommandValidator : AbstractValidator<AccountCreateCommand>
    {
        public AccountCreateCommandValidator()
        {
            RuleFor(x => x.CreateRequest.Balance).GreaterThan(0);
            RuleFor(x => x.CreateRequest.Owner).NotNull().NotEmpty();
        }
    }
}
