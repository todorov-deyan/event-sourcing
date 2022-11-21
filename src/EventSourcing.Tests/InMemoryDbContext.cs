using EventSourcing.Api.Aggregates.CustomEs.Repository;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Tests
{
    public class InMemoryDbContextFixture
    {
        private const string databaseName = "InMemoryDb";
        public CustomEsDbContext MemoryDbContext { get; init; }

        public InMemoryDbContextFixture()
        {
            MemoryDbContext = new CustomEsDbContext(
               new DbContextOptionsBuilder<CustomEsDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options);
        }
    }
}
