using EventSourcing.Api.Aggregates.CustomEs.Repository;

using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Tests.DBContexts
{
    public class PostgreDBContextFixture : PostgresSql
    {
        public CustomEsDbContext PostgreeDbContext { get; init; }

        public PostgreDBContextFixture()
        {
            PostgreeDbContext = new CustomEsDbContext(
                new DbContextOptionsBuilder<CustomEsDbContext>()
                    .UseNpgsql(ConnectionString, options => options.UseAdminDatabase("postgres"))
                    .Options);

            PostgreeDbContext.Database.EnsureDeleted();
            PostgreeDbContext.Database.EnsureCreated();
        }
    }
}
