using Events.Services;
using System;
using Xunit;

namespace EventsApiTest
{
    public class AuthServiceTest
    {
        private AuthService authService = new AuthService();

        [Theory]
        [InlineData("Basic bmFtZTpwYXNz", new string[]{"name", "pass"})]
        public void Test(string header, string[] result)
        {
            var expected = authService.getNameAndPassword(header);
            Assert.Equal(expected, result);
        }
    }
}
