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
    [Order(2)]
    public class CustomEvPostgreeTest : PostgreeDBContextBase
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

        [Fact, Order(2)]
        public void CreateAccount()
        {
            var account = new Account();

            var createEvent = new AccountCreated
            {
                Owner = "CreateTestAccount",
                Balance = 1000,
                Description = "Saved money"
            };

            _repository.Add(account, new List<IEventState> { createEvent }, default);
            var result = _repository.Find(account.Id);

            Assert.NotNull(result);
        }

        [Fact, Order(3)]
        public void ActivateAccount()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");

            var createEvent = new AccountActivated
            {
                Balance = 1000,
                Description = "Saved money. Activated"
            };

            _repository.Update(streamId, new List<IEventState> { createEvent }, default);
            var result = _repository.Find(streamId);

            Assert.NotNull(result);
        }
        
        [Fact, Order(4)]
        public void DeactivateAccount()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");

            var createEvent = new AccountCreated
            {
                Owner = "CreateTestAccount",
                Balance = 1000,
                Description = "Saved money. Deactivated"
            };

            _repository.Update(streamId, new List<IEventState> { createEvent }, default);
            var result = _repository.Find(streamId);

            Assert.NotNull(result);
        }

        [Fact, Order(5)]
        public void GetAccount_ById()
        {
            var result = _repository.Find(specialStreamId);
            
            result.ShouldNotBeNull();
        }

        [Fact, Order(6)]
        public void GetAccount_ById_Should_Throw_Exeption()
        {
            var someGuid = Guid.NewGuid();
            var result = _repository.Find(someGuid);
            
            result.ShouldThrow<ArgumentNullException>();
        }

        [Fact, Order(7)]
        public void GetAccount_ById_Reflection()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");
            var result = _repository.FindReflection(streamId);

            Assert.NotNull(result);
        }        

        [Fact, Order(8)]
        public void GetAccountAll_ById_Reflection()
        {           
        }
    }
}
