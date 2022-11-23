using Xunit;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]

namespace EventSourcing.Tests
{
    public class AssemblySettings
    {
    }
}
