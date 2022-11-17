using Ardalis.Result;

using EventSourcing.Api.Aggregates.CustomEs.ModelDto;
using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.CQRS;
using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.CustomEs.Commands
{
    public record AccountActivateCommandCustomEs(Guid Id, AccountActivateRequestCustomEs ActivateRequest) : ICommandRequest<Result<Account>>;

    public class AccountActivateCustomEsCommandHandler : ICommandHandler<AccountActivateCommandCustomEs, Result<Account>>
    {
        private readonly ICustomEsRepository<Account> _repository;

        public AccountActivateCustomEsCommandHandler(ICustomEsRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(AccountActivateCommandCustomEs request, CancellationToken cancellationToken)
        {
            var newAccount = new Account();

            AccountActivated accountCreated = new()
            {
                Balance = 10,
            };

            await _repository.Update(
                request.Id,
                new List<IEventState>() { accountCreated },
                cancellationToken: cancellationToken);

            return Result.Success(newAccount);
        }
    }
}
