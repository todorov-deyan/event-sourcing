using System.Collections;

namespace EventSourcing.Tests.TestData
{
    internal class GenTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
                { "9F67670B-89E1-4876-90DC-BB1D039217C6", "Account 1", 1000, "Saved money 1" };
            yield return new object[]
                { "47D43009-E529-42BF-9860-CD4F19F83F44", "Account 2", 2000, "Saved money 2" };
            yield return new object[]
                { "C60F7419-C0BD-4B73-A89E-D9CCFB550AC6", "Account 3", 3000, "Saved money 3" };
            yield return new object[]
                { "0FCAEEFE-01DF-48DC-9404-C194EBB8C5F3", "Account 4", 4000, "Saved money 4" };
            yield return new object[]
                { "5896B650-FB05-4FC4-925F-3B9CAF795237", "Account 5", 5000, "Saved money 5" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
