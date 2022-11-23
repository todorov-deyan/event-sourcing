using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.MartenDb.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.EventSourcing;
using EventSourcing.Tests.DBContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marten;
using Marten.Events.Projections;
using Xunit;
using Xunit.Extensions.Ordering;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
//[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]

namespace EventSourcing.Tests.MartenDb
{
    [Order(1)]
    public class MartenDBPostgreeTest : IClassFixture<MartenDbContext>
    {
        private readonly IMartenRepository<Account> _repository;

        public MartenDBPostgreeTest(MartenDbContext dbcontext)
        {
            dbcontext.UseSelfAggregate<Account>(ProjectionLifecycle.Inline);

            _repository = new MartenRepository<Account>(dbcontext.Session);
        }

        [Fact, Order(1)]
        public void Init()
        {
        }


        [Fact, Order(2)]
        public async void CreateAccount()
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

            await _repository.Add(account, new List<IEventState> { createEvent }, default);
            var result = await _repository.Find(account.Id, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact, Order(3)]
        public void ReturnBadRequest_CreationAccount()
        {
        }

        [Fact, Order(4)]
        public async void ActivateAccount()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");

            var createEvent = new AccountActivated
            {
                Balance = 1000,
                Description = "Saved money. Activated"
            };

            await _repository.Update(streamId, new List<IEventState> { createEvent }, default);
            var result = await _repository.Find(streamId, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact, Order(5)]
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
            var result = await _repository.Find(streamId, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact, Order(6)]
        public async void GetAccount_ById()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");
            var result = await _repository.Find(streamId, CancellationToken.None);

            Assert.NotNull(result);
        }


    }
}
