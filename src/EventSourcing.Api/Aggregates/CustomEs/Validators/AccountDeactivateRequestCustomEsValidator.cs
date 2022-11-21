using EventSourcing.Api.Aggregates.CustomEs.ModelDto;
using EventSourcing.Api.Common;
using FluentValidation;

namespace EventSourcing.Api.Aggregates.CustomEs.Validators
{
    public class AccountDeactivateRequestCustomEsValidator : AbstractValidator<AccountDeactivateRequestCustomEs>
    {
        public AccountDeactivateRequestCustomEsValidator()
        {
            RuleFor(x => x.Description)
              .NotEmpty()
              .WithMessage(Constants.DescriptionSymbolsRequiredErrorMessage)
              .Length(Constants.DescriptionSymbolsMinLenght, Constants.DescriptionSymbolsMaxLenght);
        }
    }
}
