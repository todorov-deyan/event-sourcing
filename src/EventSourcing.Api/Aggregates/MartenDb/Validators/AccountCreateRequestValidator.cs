using EventSourcing.Api.Aggregates.MartenDb.ModelDto;

using FluentValidation;

namespace EventSourcing.Api.Aggregates.MartenDb.Validators
{
    public class AccountCreateRequestValidator : AbstractValidator<AccountCreateRequest>
    {
        public AccountCreateRequestValidator()
        {
            RuleFor(x => x.Balance).GreaterThan(0);
            RuleFor(x => x.Owner).NotNull().NotEmpty();
        }
    }
}
