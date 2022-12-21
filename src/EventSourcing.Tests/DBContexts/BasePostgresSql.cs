using System.Data;
using System.Diagnostics;

using Marten;

using Npgsql;

namespace EventSourcing.Tests.DBContexts
{
    public abstract class PostgresSql : IDisposable
    {
        private const string PortOut = "5433";

        private const string PortIn = "5432";

        private const string TestUrl = $"http://localhost:{PortOut}/ping";

        private const string ImageName = "postgres_test";

        private const string DbName = "martendb_test";

        private const string Username = "postgres";

        private const string Password = "secretp@ssword";

        private static readonly TimeSpan TestTimeout = TimeSpan.FromSeconds(180);

        private static Process? _process;

        protected const string SchemaName = "martendb_event_sourcing";

        protected const string ConnectionString = $"PORT={PortOut};HOST=127.0.0.1;DATABASE={DbName};USER ID={Username};PASSWORD={Password}";

        protected PostgresSql()
        {
            if (_process == null)
            {
                _process = Process.Start(
                    "docker",
                    $"run --name {ImageName} -e POSTGRES_USER={Username} -e POSTGRES_PASSWORD={Password} -e POSTGRES_DB={DbName} -p {PortOut}:{PortIn} postgres:alpine");

                var started = WaitForContainer().Result;
                if (!started)
                {
                    throw new Exception($"Startup failed, could not get '{TestUrl}' after trying for '{TestTimeout}'");
                }
            }

            Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", ConnectionString);
        }

        private static async Task<bool> WaitForContainer()
        {
            var startTime = DateTime.Now;
            while (DateTime.Now - startTime < TestTimeout)
            {
                try
                {
                    await using var connection = new NpgsqlConnection(ConnectionString);

                    connection.Open();

                    if (connection.State == ConnectionState.Open)
                    {
                        return true;
                    }
                }
                catch
                {
                    // Ignore exceptions, just retry
                }

                await Task.Delay(1000).ConfigureAwait(false);
            }

            return false;
        }

        public virtual void Dispose()
        {
            var store = DocumentStore.For(ConnectionString);

            store.Advanced.Clean.CompletelyRemoveAll();

            if (_process != null)
            {
                _process.Dispose();
                _process = null;
            }

            var processStop = Process.Start("docker", $"stop {ImageName}");
            processStop.WaitForExit();

            var processRm = Process.Start("docker", $"rm {ImageName}");
            processRm.WaitForExit();
        }
    }
}
