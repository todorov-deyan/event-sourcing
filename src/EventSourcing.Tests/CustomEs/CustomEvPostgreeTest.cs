using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.EventSourcing;
using EventSourcing.Tests.DBContexts;
using Shouldly;
using System.Reflection;
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
        public void Init()
        {
        }

        public async void CreateAccount()
        {
            var account = new Account();

            var createEvent = new AccountCreated
            {
                Owner = "CreateTestAccount",
                Balance = 1000,
                Description = "Saved money"
            };

            await _repository.Add(account, new List<IEventState> { createEvent }, default);
            var result = _repository.Find(account.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async void ActivateAccount()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");

            var createEvent = new AccountActivated
            {
                Balance = 1000,
                Description = "Saved money. Activated"
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default);
            var result = _repository.Find(streamId);

            Assert.NotNull(result);
        }

        [Fact]
        public async void TryToActivateNonExistingAccount_ById()
        {
            Guid streamId = Guid.NewGuid();

            var createEvent = new AccountActivated
            {
                Balance = 1000,
                Description = "Saved money. Activated"
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default);

            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Find(streamId));
        }

        public async void DeactivateAccount()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");

            var createEvent = new AccountCreated
            {
                Owner = "CreateTestAccount",
                Balance = 1000,
                Description = "Saved money. Deactivated"
            };

           await _repository.Update(streamId, new List<IEventState> { createEvent }, default);
            var result = _repository.Find(streamId);

            Assert.NotNull(result);
        }

        public async void TryToDeactivateNonExistingAccount_ById()
        {
            Guid streamId = Guid.NewGuid();

            var createEvent = new AccountCreated
            {
                Owner = "CreateTestAccount",
                Balance = 1000,
                Description = "Saved money. Deactivated"
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default);

            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Find(streamId));
        }

        [Fact]
        public async void GetAccount_ById()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");
            var result = await _repository.Find(streamId);

            Assert.NotNull(result);
        }

        [Fact]
        public async void TryToGetNonExistingAccount_ById()
        {
            Guid streamId = Guid.NewGuid();

            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.Find(streamId));
        }

        //[Fact]
        //public void GetAccount_ById_Reflection()
        //{
        //    Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");
        //    var result = _repository.FindReflection(streamId);

        //    Assert.NotNull(result);
        //}

        //[Fact]
        //public void GetNonExistingAccount_ById_Reflection()
        //{
        //    Guid streamId = Guid.NewGuid();

        //    Assert.ThrowsAsync<ArgumentException>(() => _repository.FindReflection(streamId));
        //}

        //[Fact]
        //public void GetAccountAll_ById_Reflection()
        //{
        //    var result = _repository.FindAllReflection();

        //    Assert.NotNull(result);
        //}
    }
}
