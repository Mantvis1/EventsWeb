using Events.Models;
using Events.Models.UserModels;

namespace EventsApiTest.TestData
{
    class TestConstants
    {
        private readonly User user = new User("testUser", "testPass", false, false);
        private readonly Event @event = new Event("testTitle", "testSummary", 1007);
        private readonly Support support = new Support("testTitle", "testSummary", 1007);

        private readonly int userCount = 3;
        private readonly int eventsCount = 2;
        private readonly int supportCount = 1;

        public Event getTestEvent()
        {
            return @event;
        }

        public User getUser()
        {
            return user;
        }

        public Support getSupport()
        {
            return support;
        }

        public int getUserCount()
        {
            return userCount;
        }


        public int getEventCount()
        {
            return eventsCount;
        }


        public int getSupportCount()
        {
            return supportCount;
        }

        public string[] getNameAndPass()
        {
            return new string[] { user.Name, user.Password };
        }
    }
}
