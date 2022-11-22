
using EventSourcing.Api.Aggregates.CustomEs.Repository;
using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Tests.DBContexts
{
    public class PostgreeDBContextFixture
    {
        private const string databaseName = "PORT = 5432; HOST = 127.0.0.1; TIMEOUT = 15; POOLING = True; MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; DATABASE = 'postgres'; PASSWORD = 'secretp@ssword'; USER ID = 'postgres'";
        public CustomEsDbContext MemoryDbContext { get; init; }

        public PostgreeDBContextFixture()
        {
            MemoryDbContext = new CustomEsDbContext(
                new DbContextOptionsBuilder<CustomEsDbContext>()
                    .UseNpgsql(databaseName)
                    .Options);
        }
    }
}
