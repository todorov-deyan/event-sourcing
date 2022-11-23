using EventSourcing.Api.Aggregates.CustomEs.Repository;

using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Tests.DBContexts
{
    public class PostgreeDBContextFixture
    {
        private const string databaseName = "PORT = 5432; HOST = 127.0.0.1; TIMEOUT = 15; POOLING = TRUE; MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; DATABASE = 'postgres_test'; PASSWORD = 'secretp@ssword'; USER ID = 'postgres'";
        public CustomEsDbContext PostgreeDbContext { get; init; }

        public PostgreeDBContextFixture()
        {
            PostgreeDbContext = new CustomEsDbContext(
                new DbContextOptionsBuilder<CustomEsDbContext>()
                    .UseNpgsql(databaseName, options => options.UseAdminDatabase("postgres"))
                    .Options);
        }
    }
}
