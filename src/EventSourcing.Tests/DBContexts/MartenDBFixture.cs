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
        private DocumentStore _store;

        protected StoreOptions Options { get; } = new StoreOptions();

        protected readonly IList<IDisposable> Disposables = new List<IDisposable>();
        protected DocumentTracking DocumentTracking { get; set; } = DocumentTracking.None;

        protected IDocumentSession _session;
        protected DocumentStore DocStore
        {
            get
            {
                if (_store == null)
                {
                    _store = new DocumentStore(Options);
                    Disposables.Add(_store);
                }

                return _store;
            }
        }

        public IDocumentSession Session
        {
            get
            {
                _session = DocStore.OpenSession(DocumentTracking);
                Disposables.Add(_session);

                return _session;
            }
        }

        public MartenDbFixture()
        {
            Options.DatabaseSchemaName = _schemaName;
            Options.AutoCreateSchemaObjects = AutoCreate.All;
            Options.Connection(Constants.ConnectionString);
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
            foreach (var disposable in Disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
