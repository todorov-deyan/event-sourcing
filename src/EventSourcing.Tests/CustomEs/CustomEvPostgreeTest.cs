using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventSourcing.Tests.DBContexts;
using Xunit;

namespace EventSourcing.Tests.CustomEs
{
    public class CustomEvPostgreeTest : PostgreeDBContextBase
    {
        public CustomEvPostgreeTest(PostgreeDBContextFixture dbContext) : base(dbContext)
        {
        }



        [Fact]
        public void Test()
        {


        }
    }
}
