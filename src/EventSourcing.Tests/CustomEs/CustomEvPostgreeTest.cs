using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSourcing.Api.Aggregates.CustomEs.Repository;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.EventSourcing;
using EventSourcing.Tests.DBContexts;
using Xunit;

namespace EventSourcing.Tests.CustomEs
{
    public class CustomEvPostgreeTest : PostgreeDBContextBase
    {
        private readonly ICustomEsRepository<Account> _repository;

        public CustomEvPostgreeTest(PostgreeDBContextFixture dbContext) : base(dbContext)
        {
            _repository = new CustomEsRepository<Account>(dbContext.PostgreeDbContext, new JsonEventSerializer());
        }



        [Fact]
        public void Test()
        {


        }
    }
}
