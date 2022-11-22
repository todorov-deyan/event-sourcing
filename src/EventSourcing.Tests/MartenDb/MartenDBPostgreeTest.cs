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
using Xunit;

namespace EventSourcing.Tests.MartenDb
{
    public class MartenDBPostgreeTest : IClassFixture<MartenDBFixture>
    {

        private readonly IMartenRepository<Account> _repository;

        public MartenDBPostgreeTest(MartenDBFixture dbcontext)
        {
            _repository = new MartenRepository<Account>(dbcontext.MartenDBContext.LightweightSession());
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
            var result = _repository.Find(account.Id, CancellationToken.None);

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
            var result = _repository.Find(streamId, CancellationToken.None);

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
            var result = _repository.Find(streamId, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAccount_ById()
        {
            Guid streamId = new Guid("5d0b0dbf-365b-4fe0-85c4-c6a670a934cb");
            var result = _repository.Find(streamId, CancellationToken.None);

            Assert.NotNull(result);
        }

      
        [Fact]
        public void GetAccountAll_ById_Reflection()
        {

        }
    }
}
