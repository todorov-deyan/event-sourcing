using Xunit;

namespace EventSourcing.Tests.DBContexts
{
    public class PostgreeDBContextBase : IClassFixture<PostgreeDBContextFixture>
    {
        protected readonly PostgreeDBContextFixture _postgreeDbContext;
        
        public PostgreeDBContextBase(PostgreeDBContextFixture dbContext)
        {
            this._postgreeDbContext = dbContext;
            this._postgreeDbContext.PostgreeDbContext.Database.EnsureDeleted();
            this._postgreeDbContext.PostgreeDbContext.Database.EnsureCreated();
        }

        public void SeedDatabase()
        {
        }
    }
}
