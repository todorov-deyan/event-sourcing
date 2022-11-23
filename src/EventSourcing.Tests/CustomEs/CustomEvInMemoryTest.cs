using System.Reflection;
using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.EventSourcing;
using EventSourcing.Tests.DBContexts;
using Marten;
using Xunit;

namespace EventSourcing.Tests.CustomEs
{
    public class CustomEvInMemoryTest : InMemoryDbContext
    {
        private readonly ICustomEsRepository<Account> _repository;
        private readonly JsonEventSerializer _serializer;

        public CustomEvInMemoryTest(InMemoryDbContextFixture dbContext) : 
            base(dbContext)
        {
            _serializer = new JsonEventSerializer();
            _serializer.ScanEvents(Assembly.LoadFrom("EventSourcing.Api.dll"));
            _repository = new CustomEsRepository<Account>(dbContext.MemoryDbContext, new JsonEventSerializer());
        }

        [Fact]

        public void CreateAccount()
        {
            var account = new Account();

            var createEvent = new AccountCreated
            {
                Owner = "CreateTest",
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

        [Fact]
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

        [Fact]
        public void GetAccount_ById()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");
            var result = _repository.Find(streamId);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAccount_ById_Reflection()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");
            var result = _repository.FindReflection(streamId);

            Assert.NotNull(result);
        }


        [Fact]
        public void GetAccountAll_ById_Reflection()
        {

        }
    }
}
