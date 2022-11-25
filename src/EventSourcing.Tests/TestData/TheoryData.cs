
namespace EventSourcing.Tests.TestData
{
    public class TheoryData<T1, T2, T3, T4> : TheoryDataBase
    {
        public void Add(T1 firstData, T2 secondData, T3 thirdData, T4 fourthData)
        {
            AddRow(firstData, secondData, thirdData, fourthData);
        }
    }
}
