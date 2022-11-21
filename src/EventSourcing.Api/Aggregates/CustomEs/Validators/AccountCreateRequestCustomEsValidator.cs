using FluentValidation;
using EventSourcing.Api.Common;
using EventSourcing.Api.Aggregates.CustomEs.ModelDto;

namespace EventSourcing.Api.Aggregates.CustomEs.Validators
{
    public class AccountCreateRequestCustomEsValidator : AbstractValidator<AccountCreateRequestCustomEs>
    {
        public AccountCreateRequestCustomEsValidator()
        {
            RuleFor(x=>x.Owner)
                .NotEmpty()
                .WithMessage(Constants.OwnerNameRequiredErrorMessage)
                .Length(Constants.OwnerNameMinLenght, Constants.OwnerNameMaxLenght);

            RuleFor(x => x.Balance)
              .NotEmpty()
              .WithMessage(Constants.BalanceValueRequiredErrorMessage)
              .GreaterThanOrEqualTo(Constants.BalanceValueMinValue);

            RuleFor(x => x.Description)
              .NotEmpty()
              .WithMessage(Constants.DescriptionSymbolsRequiredErrorMessage)
              .Length(Constants.DescriptionSymbolsMinLenght, Constants.DescriptionSymbolsMaxLenght);
        }
    }
}
