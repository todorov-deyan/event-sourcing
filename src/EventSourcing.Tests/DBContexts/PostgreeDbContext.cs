using EventSourcing.Api.Aggregates.CustomEs.Repository.Entities;
using EventSourcing.Api.Aggregates.MartenDb.Events;

using Newtonsoft.Json;

using Xunit;

namespace EventSourcing.Tests.DBContexts
{
    public class PostgreeDbContext : IClassFixture<PostgreeDBContextFixture>
    {
        protected readonly PostgreeDBContextFixture _postgreeDbContext;
        
        public PostgreeDbContext(PostgreeDBContextFixture dbContext)
        {
            _postgreeDbContext = dbContext;
        }
    }
}
