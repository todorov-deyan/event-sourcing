using Ardalis.Result;

using EventSourcing.Api.Aggregates.CustomEs.ModelDto;
using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.MartenDb.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.CQRS;
using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.CustomEs.Commands
{
    public record AccountDeactivateCommandCustomEs(Guid Id, AccountDeactivateRequestCustomEs CreateRequest) : ICommandRequest<Result<Account>>;

    public class AccountDeactivateCustomEsCommandHandler : ICommandHandler<AccountDeactivateCommandCustomEs, Result<Account>>
    {
        private readonly ICustomEsRepository<Account> _repository;

        public AccountDeactivateCustomEsCommandHandler(ICustomEsRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(AccountDeactivateCommandCustomEs request, CancellationToken cancellationToken)
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
