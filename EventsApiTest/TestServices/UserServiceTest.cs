using Events.Constants;
using Events.Models.UserModels;
using Events.Services;
using EventsApiTest.TestData;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace EventsApiTest
{
    [ExcludeFromCodeCoverage]
    public class UserServiceTest : IDisposable
    {
        private AfterAndBeforeTests tests;
        private UserService userService;
        private AuthService authService;
        private TestConstants constants;

        public UserServiceTest()
        {
            tests = new AfterAndBeforeTests();
            userService = new UserService();
            authService = new AuthService();
            constants = new TestConstants();
        }

        public void Dispose()
        {
            tests.clearDB();
        }

        [Fact]
        public void UserCountInDatabase()
        {
            int actuly = userService.getListLength();
            Assert.True(constants.getUserCount() == actuly, " actuly is " + actuly);
        }

        [Fact]
        public void UserContainsToList()
        {
            User user = authService.createNewUser(constants.getUser().Name, constants.getUser().Password);
            Assert.True(userService.getAllUsers().Find(x => x.Name == user.Name && x.Password == user.Password) != null);
        }

        [Fact]
        public void CreateUser()
        {
            int userCount = userService.getListLength() + 1;
            authService.createNewUser(constants.getUser().Name, constants.getUser().Password);
            Assert.True(userCount == userService.getListLength());
        }


        [Theory]
        [InlineData("testUser", "testPass")]
        public void getUserByNameAndPassword(string uName, string pass)
        {
            var user = userService.getUserByNameAndPassword(uName, pass);
            Assert.Equal(user, userService.getUserByNameAndPassword(constants.getUser().Name, constants.getUser().Password));
        }


        [Fact]
        public void getById()
        {
            authService.createNewUser(constants.getUser().Name, constants.getUser().Password);
            var user = userService.getUserById(userService.getIdByNameAndPassword(new string[] { constants.getUser().Name, constants.getUser().Password}));
            Assert.Equal(user.Name, userService.getUserByNameAndPassword(constants.getUser().Name, constants.getUser().Password).Name);
        }

        [Fact]
        public void BanAndUnbanUser()
        {
            User user = authService.createNewUser(constants.getUser().Name, constants.getUser().Password);
            user.IsBanned = Banned.banUser();
            Assert.True(user.IsBanned == true, "user is banned");
            user.IsBanned = Banned.unbanUser();
            Assert.True(user.IsBanned == false, "user is not banned");
        }

        [Fact]
        public void createUserUsingMock()
        {
            var mockUserBuilder = new Mock<IUser>();
            mockUserBuilder.Setup(x => x.createNewUser(constants.getUser().Name, constants.getUser().Password))
                .Returns(true);
            bool isCreated = authService.createNewUser(mockUserBuilder.Object);

            Assert.True(isCreated);
        }
    }
}
