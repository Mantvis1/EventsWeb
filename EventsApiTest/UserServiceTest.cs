using Events.Services;
using Xunit;

namespace EventsApiTest
{
    public class UserServiceTest
    {
        private UserService userService = new UserService();

        [Fact]
        public void UserCountInDatabase()
        {
            int actuly = userService.getListLength();
            Assert.False(true, "not impemented");
            Assert.True(1 == actuly, " actuly is " + actuly);
        }
    }
}
