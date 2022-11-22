using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EventSourcing.Tests.DBContexts
{
    public class PostgreeDBContextBase : IClassFixture<PostgreeDBContextFixture>
    {
        protected readonly PostgreeDBContextFixture posgreeDbContext;

        public PostgreeDBContextBase(PostgreeDBContextFixture dbContext)
        {
            this.posgreeDbContext = dbContext;
        }
    }
}
