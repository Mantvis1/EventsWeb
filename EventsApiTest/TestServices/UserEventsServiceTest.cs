using EventsApiTest.TestData;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Xunit;

namespace EventsApiTest.TestServices
{
    [ExcludeFromCodeCoverage]
    public class UserEventsServiceTest
    {
        [Fact]
        public void test()
        {
            var mock = new Mock<IUserEvents>();
            mock.Setup(x => x.GetId(1)).Returns(2);
            throw new NotImplementedException();
          //  Assert.Same(mock., );
        }
    }
}
