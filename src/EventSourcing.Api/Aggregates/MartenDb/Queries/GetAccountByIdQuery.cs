using Ardalis.Result;
using EventSourcing.Api.Aggregates.MartenDb.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.CQRS;

namespace EventSourcing.Api.Aggregates.MartenDb.Queries
{
    public record GetAccountByIdQuery(Guid Id) : IQueryRequest<Result<Account>>;

    public class GetAccountByIdQueryHandlers : IQueryHandler<GetAccountByIdQuery, Result<Account>>
    {
        private readonly IMartenRepository<Account> _repository;

        public GetAccountByIdQueryHandlers(IMartenRepository<Account> repository)
        {
            _repository = repository;
        }

        public async Task<Result<Account>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.Find(request.Id, cancellationToken);
            if (result is null)
                return Result.NotFound();

            return Result<Account>.Success(result);
        }
    }
}
