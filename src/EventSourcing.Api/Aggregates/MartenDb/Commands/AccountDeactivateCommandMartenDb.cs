using Ardalis.Result;

using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.MartenDb.ModelDto;
using EventSourcing.Api.Aggregates.MartenDb.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.CQRS;
using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.MartenDb.Commands
{
    public record AccountDeactivateCommandMartenDb(Guid Id, AccountDeactivateRequestMartenDb CreateRequest) : ICommandRequest<Result<Account>>;

    public class AccountDeactivateMartenDbCommandHandler : ICommandHandler<AccountDeactivateCommandMartenDb, Result<Account>>
    {
        private readonly IMartenRepository<Account> _repository;

        public AccountDeactivateMartenDbCommandHandler(IMartenRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(AccountDeactivateCommandMartenDb request, CancellationToken cancellationToken)
        {
            var newAccount = new Account();
         
            AccountDeactivated accountCreated = new()
            {   
            };
            
            await _repository.Update(
                request.Id, 
                new List<IEventState>() { accountCreated }, 
                cancellationToken: cancellationToken);
            
            return Result.Success(newAccount);
        }
    }
}
