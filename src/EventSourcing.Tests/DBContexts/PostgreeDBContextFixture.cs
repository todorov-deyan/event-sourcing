using EventSourcing.Api.Aggregates.CustomEs.Repository;

using Microsoft.EntityFrameworkCore;

namespace EventSourcing.Tests.DBContexts
{
    public class PostgreeDBContextFixture
    {
        public CustomEsDbContext PostgreeDbContext { get; init; }

        public PostgreeDBContextFixture()
        {
            PostgreeDbContext = new CustomEsDbContext(
                new DbContextOptionsBuilder<CustomEsDbContext>()
                    .UseNpgsql(Constants.ConnectionString, options => options.UseAdminDatabase("postgres"))
                    .Options);

            PostgreeDbContext.Database.EnsureDeleted();
            PostgreeDbContext.Database.EnsureCreated();
        }
    }
}
