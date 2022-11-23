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

namespace EventSourcing.Tests.DBContexts
{
    public class PostgreeDbContext : IClassFixture<PostgreeDBContextFixture>
    {
        protected readonly PostgreeDBContextFixture _postgreeDbContext;
        
        public PostgreeDbContext(PostgreeDBContextFixture dbContext)
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
