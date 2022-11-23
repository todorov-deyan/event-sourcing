using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.MartenDb.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.EventSourcing;
using EventSourcing.Tests.DBContexts;

using Marten.Events.Projections;

using Xunit;
using Xunit.Extensions.Ordering;

namespace EventSourcing.Tests.MartenDb
{
    public class MartenDBPostgreeTest : IClassFixture<MartenDbContext>
    {
        private readonly IMartenRepository<Account> _repository;

        public MartenDBPostgreeTest(MartenDbContext dbcontext)
        {
            dbcontext.UseSelfAggregate<Account>(ProjectionLifecycle.Inline);

            _repository = new MartenRepository<Account>(dbcontext.Session);
        }

        [Fact, Order(1)]
        public void Dummy_Init()
        {
        }


        [Fact, Order(2)]
        public async Task CreateAccount()
        {
            var account = new Account
            {
                Id = Guid.NewGuid(),
                Owner = "TestCreate",
                Balance = 10000
            };

            var createEvent = new AccountCreated
            {
                Owner = "CreateTest",
                Balance = 1000,
                Description = "Saved money"
            };

            await _repository.Add(account, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = await _repository.Find(account.Id, CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Fact, Order(3)]
        public async Task ActivateAccount()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");

            var createEvent = new AccountActivated
            {
                Balance = 1000,
                Description = "Saved money. Activated"
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = await _repository.Find(streamId, CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Fact, Order(4)]
        public async Task GetAccount_ById()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");
            var result = await _repository.Find(streamId, CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Fact, Order(5)]
        public async Task TryToActivateNonExistingAccount_ById()
        {
            //TODO:
        }

        [Fact, Order(6)]
        public async Task TryToDeactivateNonExistingAccount_ById()
        {
            //TODO:
        }

        [Fact, Order(7)]
        public async Task TryToGetNonExistingAccount_ById()
        {
            //TODO:
        }

        [Fact, Order(8)]
        public async Task DeactivateAccount()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");

            var createEvent = new AccountDeactivated
            {
                ClosingBalance = 0,
                Description = "Saved money. Deactivated"
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = await _repository.Find(streamId, CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(result);
        }
    }
}
