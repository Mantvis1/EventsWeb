using Events.Models.UserModels;
using Events.Services;
using System;
using System.Diagnostics;
using Xunit;
using Xunit.Sdk;

namespace EventsApiTest
{
    public class UserServiceTest : IDisposable
    {
        private AfterAndBeforeTests tests;
        private UserService userService;
        private AuthService authService;

        public UserServiceTest()
        {
            tests = new AfterAndBeforeTests();
            userService = new UserService();
            authService = new AuthService();
        }

        public void Dispose()
        {
            tests.clearDB();
        }

        [Fact]
        public void UserCountInDatabase()
        {
            int actuly = userService.getListLength();
            Assert.True(3 == actuly, " actuly is " + actuly);
        }

        [Fact]
        public void CreateUser()
        {
            int userCount = userService.getListLength() + 1;
            authService.createNewUser(tests.getTestUser()[0], tests.getTestUser()[1]);
            Assert.True(userCount == userService.getListLength());
        }


        [Theory]
        [InlineData("name","pass")]
        public void getUserByNameAndPassword(string uName, string pass)
        {
            var user = userService.getUserByNameAndPassword(uName, pass);
            Assert.Equal(user, userService.getUserByNameAndPassword(tests.getTestUser()[0], tests.getTestUser()[1]));
        }


        [Fact]
        public void getById()
        {
            var user = userService.getUserById(1012);
            Assert.Equal(user.Name, userService.getUserByNameAndPassword(tests.getTestUser()[0], tests.getTestUser()[1]).Name);
        }
    }
}
