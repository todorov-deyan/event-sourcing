using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSourcing.Api.Aggregates.CustomEs.Repository.Entities;
using EventSourcing.Api.Aggregates.MartenDb.Events;
using EventSourcing.Api.Aggregates.Model;
using EventSourcing.Api.Common.EventSourcing;
using Xunit;
using Marten;
using Newtonsoft.Json;

namespace EventSourcing.Tests.DBContexts
{
    public class PostgreeDBContextBase : IClassFixture<PostgreeDBContextFixture>
    {
        public Guid SeededStreamId = Guid.NewGuid();

        protected readonly PostgreeDBContextFixture _postgreeDbContext;

        public PostgreeDBContextBase(PostgreeDBContextFixture dbContext)
        {
            _postgreeDbContext = dbContext;

            _postgreeDbContext.PostgreeDbContext.Database.EnsureDeleted();
            _postgreeDbContext.PostgreeDbContext.Database.EnsureCreated();
        }

        public void SeedDatabase()
        {
            var createEvent = new AccountCreated
            {
                Owner = "Miro",
                Balance = 555,
                Description = "Deposit"
            };

            _postgreeDbContext.PostgreeDbContext.AddRange(
                new CustomStream
                {
                    StreamId = SeededStreamId,
                    CreatedAt = DateTime.UtcNow,
                    Type = "account",
                    Events = new List<CustomEvent>()
                    {
                        new CustomEvent()
                        {
                            EventId = Guid.NewGuid(),
                            StreamId = SeededStreamId,
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
