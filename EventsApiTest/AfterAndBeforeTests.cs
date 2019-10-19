using Events.Services;

namespace EventsApiTest
{
    class AfterAndBeforeTests
    {
        private string[] testUser = new string[] { "testName", "testPass" };

        private UserService userService = new UserService();

        public void clearDB()
        {
            while (userService.getUserByNameAndPassword(getTestUser()[0], getTestUser()[1]) != null)
            {
                userService.deleteUserById(userService.getIdByNameAndPassword(testUser));
            }
        }

        public string[] getTestUser()
        {
            return testUser;
        }
    }
}
