using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.MartenDb.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.EventSourcing;
using EventSourcing.Tests.DBContexts;
using EventSourcing.Tests.TestData;
using Marten.Events.Projections;

using Xunit;
using Xunit.Extensions.Ordering;

namespace EventSourcing.Tests.MartenDb
{
    [Collection(nameof(MartenDBPostgreeTest))]
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


        [Theory, Order(2)]
        [ClassData(typeof(GenTestData))]
        public async Task CreateAccount(Guid streamId, string owner, decimal balance, string description)
        {
            var account = new Account
            {
                Id = streamId,
            };

            var createEvent = new AccountCreated
            {
                Owner = owner,
                Balance = balance,
                Description = description
            };

            await _repository.Add(account, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = await _repository.Find(account.Id, CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(result);           
        }

        [Theory, Order(3)]
        [ClassData(typeof(GenTestData))]
        public async Task ActivateAccount(Guid streamId, string owner, decimal balance, string description)
        {
            var createEvent = new AccountActivated
            {
                Balance = 1000,
                Description = description
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = await _repository.Find(streamId, CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Theory, Order(4)]
        [ClassData(typeof(GenTestData))]
        public async Task GetAccount_ById(Guid streamId, string owner, decimal balance, string description)
        {
            var result = await _repository.Find(streamId, CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(result.Id);
        }

        [Theory, Order(5)]
        [ClassData(typeof(GenTestData))]
        public async Task TryToActivateNonExistingAccount_ById(Guid streamId, string owner, decimal balance, string description)
        {
            var createEvent = new AccountActivated
            {
                Balance = 1000,
                Description = description
            };

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(streamId, CancellationToken.None).ConfigureAwait(false));
        }

        [Theory, Order(6)]
        [ClassData(typeof(GenTestData))]
        public async Task TryToDeactivateNonExistingAccount_ById(Guid streamId, string owner, decimal balance, string description)
        {
       var createEvent = new AccountDeactivated
            {
                ClosingBalance = balance,
                Description = "Closed. Deactivated"
            };

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(streamId, CancellationToken.None).ConfigureAwait(false));
        }

        [Theory, Order(7)]
        [ClassData(typeof(GenTestData))]
        public async Task TryToGetNonExistingAccount_ById(Guid streamId, string owner, decimal balance, string description)
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(streamId, CancellationToken.None).ConfigureAwait(false));
        }

        [Theory, Order(8)]
        [ClassData(typeof(GenTestData))]
        public async Task DeactivateAccount(Guid streamId, string owner, decimal balance, string description)
        {
            var createEvent = new AccountDeactivated
            {
                ClosingBalance = balance,
                Description = description
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = await _repository.Find(streamId, CancellationToken.None).ConfigureAwait(false);

            Assert.NotNull(result);
        }
    }
}
