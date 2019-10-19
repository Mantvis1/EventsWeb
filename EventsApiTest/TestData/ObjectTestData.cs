using Events.Models.UserModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EventsApiTest.TestData
{
    public class ObjectTestData : IEnumerable<object[]>
    {
        private User testUser = new User("name", "pass", false, true);

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { testUser, true };
            yield return new object[] { null, false };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
