using Ardalis.Result;

using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.MartenDb.ModelDto;
using EventSourcing.Api.Aggregates.MartenDb.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.CQRS;
using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.MartenDb.Commands
{
    public record AccountDeactivateCommand(Guid Id, AccountDeactivateRequest CreateRequest) : ICommandRequest<Result<Account>>;

    public class AccountDeactivateCommandHandler : ICommandHandler<AccountDeactivateCommand, Result<Account>>
    {
        private readonly IMartenRepository<Account> _repository;

        public AccountDeactivateCommandHandler(IMartenRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(AccountDeactivateCommand request, CancellationToken cancellationToken)
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
