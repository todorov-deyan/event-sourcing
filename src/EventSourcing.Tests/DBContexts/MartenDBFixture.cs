using EventSourcing.Api.Common.EventSourcing;
using Marten;
using Marten.Events.Projections;
using Weasel.Core;
using StoreOptions = Marten.StoreOptions;

namespace EventSourcing.Tests.DBContexts
{
    public class MartenDbFixture  : IDisposable
    {
        private const string _schemaName = "martendb_event_sourcing";
        private const string _databaseName = "PORT = 5432; HOST = 127.0.0.1; TIMEOUT = 15; POOLING = TRUE; MINPOOLSIZE = 1; MAXPOOLSIZE = 100; COMMANDTIMEOUT = 20; DATABASE = 'postgres_test'; PASSWORD = 'secretp@ssword'; USER ID = 'postgres'";

        protected StoreOptions Options { get; } = new StoreOptions();
        private DocumentStore _store;

        protected DocumentStore DocStore
        {
            get
            {
                if (_store == null)
                {
                    _store = new DocumentStore(Options);
                }

                return _store;
            }
        }

        public MartenDbFixture()
        {
            Options.DatabaseSchemaName = _schemaName;
            Options.AutoCreateSchemaObjects = AutoCreate.All;
            Options.Connection(_databaseName);
            Options.UseDefaultSerialization(EnumStorage.AsString, nonPublicMembersStorage: NonPublicMembersStorage.All);

            DocStore.Advanced.Clean.CompletelyRemoveAll();
        }

        public void UseSelfAggregate<T>(ProjectionLifecycle? lifecycle = null) where T : Aggregate
        {
            Options.Projections.SelfAggregate<T>(lifecycle);
        }

        public void UseProjection<T>() where T : IProjection, new()
        {
            Options.Projections.Add(new T());
        }
        
        public void Dispose()
        {
            _store?.Dispose();
        }
    }
}
