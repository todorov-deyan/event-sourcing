using EventSourcing.Api.Aggregates.CustomEs.Repository.Entities;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using Newtonsoft.Json;
using Xunit;

namespace EventSourcing.Tests.DBContexts
{
    public class PostgreeDBContextBase : IClassFixture<PostgreeDBContextFixture>
    {
        public Guid specialStreamId = Guid.NewGuid();

        protected readonly PostgreeDBContextFixture _postgreeDbContext;

        public PostgreeDBContextBase(PostgreeDBContextFixture dbContext)
        {
            _postgreeDbContext = dbContext;

            _postgreeDbContext.PostgreeDbContext.Database.EnsureDeleted();
            _postgreeDbContext.PostgreeDbContext.Database.EnsureCreated();
        }

        public void SeedDatabase()
        {
            foreach (int stream in Enumerable.Range(1, 10))
            {
                var createEvent = new AccountCreated
                {
                    Owner = "Miro",
                    Balance = 5 * stream,
                    Description = "Deposit"
                };

                foreach (int _event in Enumerable.Range(1, 10))
                {
                    Guid newStreamId = Guid.NewGuid();
                    if(stream == 2 && _event == 2)
                    {
                        newStreamId = specialStreamId;
                    }

                    _postgreeDbContext.PostgreeDbContext.AddRange(
                        new CustomStream
                        {
                            StreamId = newStreamId,
                            CreatedAt = DateTime.UtcNow,
                            Type = "account",
                            Events = new List<CustomEvent>()
                            {
                                        new CustomEvent()
                                        {
                                            EventId = Guid.NewGuid(),
                                            StreamId = newStreamId,
                                            EventType = "accountcreated",
                                            CreatedAt = DateTime.UtcNow,
                                            Data = JsonConvert.SerializeObject(createEvent)
                                        }
                            }
                        });

                    _postgreeDbContext.PostgreeDbContext.SaveChanges();
                }
            }
        }
    }
}
