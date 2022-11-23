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
            var result = _repository.Find(account.Id).ConfigureAwait(false);

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
            var result = _repository.Find(streamId).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Fact, Order(4)]
        public async Task GetAccount_ById()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");
            var result = await _repository.Find(streamId).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Fact, Order(5)]
        public async Task TryToActivateNonExistingAccount_ById()
        {
            Guid streamId = Guid.NewGuid();

            var createEvent = new AccountActivated
            {
                Balance = 1000,
                Description = "Saved money. Activated"
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(streamId).ConfigureAwait(false));
        }

        [Fact, Order(6)]
        public async Task TryToDeactivateNonExistingAccount_ById()
        {
            Guid streamId = Guid.NewGuid();

            var createEvent = new AccountCreated
            {
                Owner = "CreateTestAccount",
                Balance = 1000,
                Description = "Saved money. Deactivated"
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(streamId).ConfigureAwait(false));
        }

        [Fact, Order(7)]
        public async Task TryToGetNonExistingAccount_ById()
        {
            Guid streamId = Guid.NewGuid();

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(streamId).ConfigureAwait(false));
        }

        [Fact, Order(8)]
        public async Task DeactivateAccount()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");

            var createEvent = new AccountCreated
            {
                Owner = "CreateTestAccount",
                Balance = 1000,
                Description = "Saved money. Deactivated"
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = await _repository.Find(streamId).ConfigureAwait(false);

            Assert.NotNull(result);
        }
    }
}
