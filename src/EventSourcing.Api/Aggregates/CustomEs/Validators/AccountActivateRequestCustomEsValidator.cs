using EventSourcing.Api.Aggregates.CustomEs.ModelDto;
using EventSourcing.Api.Common;
using FluentValidation;

namespace EventSourcing.Api.Aggregates.CustomEs.Validators
{
    public class AccountActivateRequestCustomEsValidator : AbstractValidator<AccountActivateRequestCustomEs>
    {
        public AccountActivateRequestCustomEsValidator()
        {
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
