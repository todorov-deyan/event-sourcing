using Marten;

namespace EventSourcing.Tests.DBContexts
{
    public class MartenDbContext : MartenDbFixture
    {
        protected IDocumentSession _session;

        protected readonly IList<IDisposable> Disposables = new List<IDisposable>();
        
        protected DocumentTracking DocumentTracking { get; set; } = DocumentTracking.None;

        public IDocumentSession Session
        {
            get
            {
                _session = base.DocStore.OpenSession(DocumentTracking);
                Disposables.Add(_session);
                
                return _session;
            }
        }

        public MartenDbContext() : base()
        {
        }

        public virtual void Dispose()
        {
            foreach (var disposable in Disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
