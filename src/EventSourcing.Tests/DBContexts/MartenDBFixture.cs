using EventSourcing.Api.Common.EventSourcing;

using Marten;
using Marten.Events.Projections;

using Weasel.Core;

using StoreOptions = Marten.StoreOptions;

namespace EventSourcing.Tests.DBContexts
{
    public class MartenDbFixture : PostgresSql, IDisposable
    {
        private DocumentStore _store;

        private StoreOptions Options { get; } = new StoreOptions();

        private readonly IList<IDisposable> Disposables = new List<IDisposable>();

        private DocumentTracking DocumentTracking { get; set; } = DocumentTracking.None;

        private IDocumentSession _session;

        protected DocumentStore DocumentStore
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
                _session = DocumentStore.OpenSession(DocumentTracking);
                Disposables.Add(_session);

                return _session;
            }
        }

        public MartenDbFixture()
        {
            Options.DatabaseSchemaName = SchemaName;
            Options.AutoCreateSchemaObjects = AutoCreate.All;
            Options.Connection(ConnectionString);
            Options.UseDefaultSerialization(EnumStorage.AsString, nonPublicMembersStorage: NonPublicMembersStorage.All);

            DocumentStore.Advanced.Clean.CompletelyRemoveAll();
        }

        public void UseSelfAggregate<T>(ProjectionLifecycle? lifecycle = null) where T : Aggregate
        {
            Options.Projections.SelfAggregate<T>(lifecycle);
        }

        public void UseProjection<T>() where T : IProjection, new()
        {
            Options.Projections.Add(new T());
        }

        public override void Dispose()
        {
            foreach (var item in Disposables)
            {
                item.Dispose();
            }

            base.Dispose();
        }
    }
}
