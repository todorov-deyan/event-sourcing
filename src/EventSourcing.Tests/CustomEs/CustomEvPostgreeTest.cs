using System.Reflection;

using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.EventSourcing;
using EventSourcing.Tests.DBContexts;
using EventSourcing.Tests.TestData;
using Xunit;
using Xunit.Extensions.Ordering;

namespace EventSourcing.Tests.CustomEs
{
    public class CustomEvPostgreeTest : IClassFixture<PostgreDBContextFixture>
    {
        private readonly ICustomEsRepository<Account> _repository;
        private readonly JsonEventSerializer _serializer;

        public CustomEvPostgreeTest(PostgreDBContextFixture dbContext) 
        {
            _serializer = new JsonEventSerializer();
            _repository = new CustomEsRepository<Account>(dbContext.PostgreeDbContext, _serializer);
        }

        [Fact, Order(1)]
        public void Dummy_Init()
        {
        }

        [Theory, Order(2)]
        [ClassData(typeof(TheoryTestData))]
        public async Task CreateAccount(Guid streamId, string owner, decimal balance, string description)
        {
            var account = new Account();
            account.Id = streamId;

            var createEvent = new AccountCreated
            {
                Owner = owner,
                Balance = balance,
                Description = description
            };

            await _repository.Add(account, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = await _repository.Find(account.Id).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Theory, Order(3)]
        [ClassData(typeof(TheoryTestData))]
        public async Task ActivateAccount(Guid streamId, string owner, decimal balance, string description)
        {
            var createEvent = new AccountActivated
            {
                Balance = 1000,
                Description = description
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = _repository.Find(streamId).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Theory, Order(4)]
        [ClassData(typeof(TheoryTestData))]
        public async Task GetAccount_ById(Guid streamId, string owner, decimal balance, string description)
        {
            var result = await _repository.Find(streamId).ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Theory, Order(5)]
        [ClassData(typeof(TheoryTestData))]
        public async Task TryToActivateNonExistingAccount_ById(Guid streamId, string owner, decimal balance, string description)
        {
            var createEvent = new AccountActivated
            {
                Balance = balance,
                Description = description
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(streamId).ConfigureAwait(false));
        }

        [Theory, Order(6)]
        [ClassData(typeof(TheoryTestData))]
        public async Task TryToDeactivateNonExistingAccount_ById(Guid streamId, string owner, decimal balance, string description)
        {
            var createEvent = new AccountDeactivated
            {
                ClosingBalance = balance,
                Description = description
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(streamId).ConfigureAwait(false));
        }

        [Theory, Order(7)]
        [ClassData(typeof(TheoryTestData))]
        public async Task TryToGetNonExistingAccount_ById(Guid streamId, string owner, decimal balance, string description)
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _repository.Find(streamId).ConfigureAwait(false));
        }

        [Theory, Order(8)]
        [ClassData(typeof(TheoryTestData))]
        public async Task DeactivateAccount(Guid streamId, string owner, decimal balance, string description)
        {
            var createEvent = new AccountDeactivated
            {
                ClosingBalance = balance,
                Description = description
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default).ConfigureAwait(false);
            var result = await _repository.Find(streamId).ConfigureAwait(false);

            Assert.NotNull(result);
        }
    }
}
