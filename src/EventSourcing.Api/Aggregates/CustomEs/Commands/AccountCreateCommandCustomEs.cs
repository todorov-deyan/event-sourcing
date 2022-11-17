using Ardalis.Result;

using EventSourcing.Api.Aggregates.CustomEs.ModelDto;
using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.CQRS;
using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.CustomEs.Commands
{
    public record AccountCreateCommandCustomEs(AccountCreateRequestCustomEs CreateRequest) : ICommandRequest<Result<Account>>;

    public class AccountCreateCustomEsCommandHandler : ICommandHandler<AccountCreateCommandCustomEs, Result<Account>>
    {
        private readonly ICustomEsRepository<Account> _repository;

        public AccountCreateCustomEsCommandHandler(ICustomEsRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(AccountCreateCommandCustomEs request, CancellationToken cancellationToken)
        {
            AccountCreated accountCreated = new()
            {
                Owner = request.CreateRequest.Owner,
            };

            var newAccount = new Account();
            newAccount.Owner = accountCreated.Owner;
            newAccount.Balance = request.CreateRequest.Balance;
            newAccount.Status = AccountStatus.Created;

            await _repository.Add(newAccount,
                                  new List<IEventState>() { accountCreated },
                                  cancellationToken: cancellationToken);

            return Result.Success(newAccount);
        }
    }
}
