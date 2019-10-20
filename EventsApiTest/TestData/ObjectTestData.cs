using System.Collections;
using System.Collections.Generic;

namespace EventsApiTest.TestData
{
    public class ObjectTestData : IEnumerable<object[]>
    {
        private TestConstants constants = new TestConstants();

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { constants.getUser(), true };
            yield return new object[] { null, false };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
