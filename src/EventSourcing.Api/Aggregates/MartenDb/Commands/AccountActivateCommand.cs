﻿using Ardalis.Result;

using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.MartenDb.ModelDto;
using EventSourcing.Api.Aggregates.MartenDb.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.CQRS;
using EventSourcing.Api.Common.EventSourcing;

namespace EventSourcing.Api.Aggregates.MartenDb.Commands
{
    public record AccountActivateCommand(Guid Id, AccountActivateRequest ActivateRequest) : ICommandRequest<Result<Account>>;

    public class AccountActivateCommandHandler : ICommandHandler<AccountActivateCommand, Result<Account>>
    {
        private readonly IMartenRepository<Account> _repository;

        public AccountActivateCommandHandler(IMartenRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(AccountActivateCommand request, CancellationToken cancellationToken)
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
