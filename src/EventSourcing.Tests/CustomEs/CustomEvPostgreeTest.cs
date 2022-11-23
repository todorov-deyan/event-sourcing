using System.Reflection;

using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.EventSourcing;
using EventSourcing.Tests.DBContexts;

using Xunit;
using Xunit.Extensions.Ordering;

namespace EventSourcing.Tests.CustomEs
{
    [Collection(nameof(CustomEvPostgreeTest))]
    public class CustomEvPostgreeTest : PostgreeDbContext
    {
        private readonly ICustomEsRepository<Account> _repository;
        private readonly JsonEventSerializer _serializer;

        public CustomEvPostgreeTest(PostgreeDBContextFixture dbContext) : base(dbContext)
        {
            _serializer = new JsonEventSerializer();
            _serializer.ScanEvents(Assembly.LoadFrom("EventSourcing.Api.dll"));

            _repository = new CustomEsRepository<Account>(dbContext.PostgreeDbContext, _serializer);
            SeedDatabase();
        }

        [Fact, Order(1)]
        public void Dummy_Init()
        {
        }

        [Fact, Order(2)]
        public async Task CreateAccount()
        {
            var account = new Account();

            var createEvent = new AccountCreated
            {
                Owner = "CreateTestAccount",
                Balance = 1000,
                Description = "Saved money"
            };

            await _repository.Add(account, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = await _repository.Find(account.Id).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Fact, Order(3)]
        public async Task ActivateAccount()
        {
            var createEvent = new AccountActivated
            {
                Balance = 1000,
                Description = "Saved money. Activated"
            };

            await _repository.Update(StreamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = _repository.Find(StreamId).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Fact, Order(4)]
        public async Task GetAccount_ById()
        {
            var result = await _repository.Find(StreamId).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Fact, Order(5)]
        public async Task TryToActivateNonExistingAccount_ById()
        {
            var createEvent = new AccountActivated
            {
                Balance = 1000,
                Description = "Saved money. Activated"
            };

            await _repository.Update(StreamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(StreamId).ConfigureAwait(false));
        }

        [Fact, Order(6)]
        public async Task TryToDeactivateNonExistingAccount_ById()
        {
            var createEvent = new AccountCreated
            {
                Owner = "CreateTestAccount",
                Balance = 1000,
                Description = "Saved money. Deactivated"
            };

            await _repository.Update(StreamId, new List<IEventState> { createEvent }, default);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(StreamId).ConfigureAwait(false));
        }

        [Fact, Order(7)]
        public async Task TryToGetNonExistingAccount_ById()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(StreamId).ConfigureAwait(false));
        }

        [Fact, Order(8)]
        public async Task DeactivateAccount()
        {
            var createEvent = new AccountCreated
            {
                Owner = "CreateTestAccount",
                Balance = 1000,
                Description = "Saved money. Deactivated"
            };

            await _repository.Update(StreamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = await _repository.Find(StreamId).ConfigureAwait(false);

            Assert.NotNull(result);
        }
    }
}
