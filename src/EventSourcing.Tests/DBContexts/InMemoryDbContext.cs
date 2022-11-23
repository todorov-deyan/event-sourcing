using Xunit;

namespace EventSourcing.Tests.DBContexts
{
    public class InMemoryDbContext : IClassFixture<InMemoryDbContextFixture>
    {
        protected readonly InMemoryDbContextFixture inmemoryDbContext;

        public InMemoryDbContext(InMemoryDbContextFixture inmemoryDbContext)
        {
            this.inmemoryDbContext = inmemoryDbContext;
        }
    }
}
