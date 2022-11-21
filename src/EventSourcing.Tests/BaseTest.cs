using Xunit;

namespace EventSourcing.Tests
{
    public class BaseTest : IClassFixture<InMemoryDbContextFixture>
    {
        protected readonly InMemoryDbContextFixture inmemoryDbContext;

        public BaseTest(InMemoryDbContextFixture inmemoryDbContext)
        {
            this.inmemoryDbContext = inmemoryDbContext;
        }
    }
}
