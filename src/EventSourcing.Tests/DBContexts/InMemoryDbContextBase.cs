using Xunit;

namespace EventSourcing.Tests.DBContexts
{
    public class InMemoryDbContextBase : IClassFixture<InMemoryDbContextFixture>
    {
        protected readonly InMemoryDbContextFixture inmemoryDbContext;

        public InMemoryDbContextBase(InMemoryDbContextFixture inmemoryDbContext)
        {
            this.inmemoryDbContext = inmemoryDbContext;
        }
    }
}
