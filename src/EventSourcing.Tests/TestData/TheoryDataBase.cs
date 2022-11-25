using System.Collections;


namespace EventSourcing.Tests.TestData
{
    public abstract class TheoryDataBase : IEnumerable<object[]>
    {
        readonly List<object[]> data = new List<object[]>();

        protected void AddRow(params object[] values)
        {
            data.Add(values);
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
