using Ardalis.Result;

using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.MartenDb.ModelDto;
using EventSourcing.Api.Aggregates.MartenDb.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.CQRS;
using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.MartenDb.Commands
{
    public record AccountCreateCommand(AccountCreateRequest CreateRequest) : ICommandRequest<Result<Account>>;

    public class AccountCreateCommandHandler : ICommandHandler<AccountCreateCommand, Result<Account>>
    {
        private readonly IMartenRepository<Account> _repository;

        public AccountCreateCommandHandler(IMartenRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(AccountCreateCommand request, CancellationToken cancellationToken)
        {
            var newAccount = new Account();
         
            AccountCreated accountCreated = new()
            {
                Owner = request.CreateRequest.Owner,
            };
            
            await _repository.Add(
                newAccount, 
                new List<IEventState>() { accountCreated }, 
                cancellationToken: cancellationToken);
            
            return Result.Success(newAccount);
        }
    }
}
