using Events.Models;
using Events.Services;
using Xunit;

namespace EventsApiTest
{
    public class ErrorServiceTest
    {
        private ErrorService errorService;

        public ErrorServiceTest()
        {
            errorService = new ErrorService();
        }

        [Fact]
        public void IsReturnTypeCorrect()
        {
            var actual = ErrorService.GetError("test");
            var expected = typeof(Error);
            Assert.IsType(expected, actual);
        }

        [Fact]
        public void IsMessageCorrect()
        {
            string message = ErrorService.GetError("testMessage").message;
            Assert.Equal("testMessage", message);
        }
    }
}
