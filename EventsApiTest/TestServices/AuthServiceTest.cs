using Events.Services;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace EventsApiTest
{
    [ExcludeFromCodeCoverage]
    public class AuthServiceTest : IDisposable
    {
        private AuthService authService;
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
        [InlineData("Basic bmFtZTpwYXNz", new string[] { "name", "pass" })]
        public void Test(string header, string[] result)
        {
            var expected = authService.getNameAndPassword(header);
            Assert.Equal(expected, result);
        }
    }
}
