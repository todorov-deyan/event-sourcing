using EventSourcing.Api.Aggregates.CustomEs.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marten;

namespace EventSourcing.Tests.DBContexts
{
    public  class MartenDBFixture  : IDisposable
    {
        private const string databaseName = "PORT = 5432; HOST = 127.0.0.1; TIMEOUT = 15; POOLING = TRUE; MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; DATABASE = 'postgres'; PASSWORD = 'secretp@ssword'; USER ID = 'postgres'";
        public IDocumentStore MartenDBContext { get; init; }

        public MartenDBFixture()
        {
            MartenDBContext = DocumentStore.For(databaseName);
        }

        public void Dispose()
        {
            MartenDBContext.Dispose();
        }
    }
}
