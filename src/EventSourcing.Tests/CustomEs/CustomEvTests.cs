using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.EventSourcing;
using Xunit;

namespace EventSourcing.Tests.CustomEs
{
    public class CustomEvTests : BaseTest
    {
        private readonly ICustomEsRepository<Account> _repository;

        public CustomEvTests(InMemoryDbContextFixture inmemoryDbContext) : 
            base(inmemoryDbContext)
        {
            _repository = new CustomEsRepository<Account>(inmemoryDbContext.MemoryDbContext, new JsonEventSerializer());
        }

        [Fact]

        public void CreateAccount()
        {
            var account = new Account();

            var createEvent = new AccountCreated
            {
                Owner = "Zori",
                Balance = 1000,
                Description = "Saved money"
            };

            _repository.Add(account, new List<IEventState> { createEvent }, default);
            var result = _repository.Find(account.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public void ReturnBadRequest_CreationAccount()
        {
        }
    }
}
