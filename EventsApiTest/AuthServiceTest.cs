using Events.Services;
using System;
using Xunit;

namespace EventsApiTest
{
    public class AuthServiceTest :IDisposable
    {
        protected AuthService authService;
        private AfterAndBeforeTests tests;

        public AuthServiceTest()
        {
            authService = new AuthService();
            tests = new AfterAndBeforeTests();
        }

        public void Dispose()
        {
            tests.clearDB();
        }

        [Theory]
        [InlineData("Basic bmFtZTpwYXNz", new string[]{"name", "pass"})]
        public void Test(string header, string[] result)
        {
            var expected = authService.getNameAndPassword(header);
            Assert.Equal(expected, result);
        }
    }
}
