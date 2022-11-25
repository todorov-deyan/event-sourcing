using Xunit;

namespace EventSourcing.Tests.TestData
{
    internal class TheoryTestData : TheoryData<Guid, string, int, string>
    {
        public TheoryTestData()
        {
            Add(new Guid("9F67670B-89E1-4876-90DC-BB1D039217C6"), "Account 1", 1000, "Saved money 1");
            Add(new Guid("47D43009-E529-42BF-9860-CD4F19F83F44"), "Account 2", 2000, "Saved money 2");
            Add(new Guid("C60F7419-C0BD-4B73-A89E-D9CCFB550AC6"), "Account 3", 2000, "Saved money 3");
            Add(new Guid("0FCAEEFE-01DF-48DC-9404-C194EBB8C5F3"), "Account 4", 2000, "Saved money 4");
            Add(new Guid("5896B650-FB05-4FC4-925F-3B9CAF795237"), "Account 6", 2000, "Saved money 5");
        }
    }
}
