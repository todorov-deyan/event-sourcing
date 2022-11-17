using Ardalis.Result;
using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.CQRS;

namespace EventSourcing.Api.Aggregates.CustomEs.Queries
{
    public record GetAccountByIdCustomEs(Guid Id) : IQueryRequest<Result<Account>>;

    public class GetAccountByIdQueryHandlers : IQueryHandler<GetAccountByIdCustomEs, Result<Account>>
    {
        private readonly ICustomEsRepository<Account> _repository;

        public GetAccountByIdQueryHandlers(ICustomEsRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(GetAccountByIdCustomEs request, CancellationToken cancellationToken)
        {
            var result = await _repository.Find(request.Id, cancellationToken);
            if (result is null)
                return Result.NotFound();

            return Result<Account>.Success(result);
        }
    }
}
