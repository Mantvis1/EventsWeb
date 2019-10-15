using Events.Models.UserModels;
using Events.Services;
using System;
using Xunit;

namespace EventsApiTest
{
    public class UserValidationServiceTest
    {
        private UserValidationService validationService = new UserValidationService();
        private User testUser = new User("name", "pass", false, true);

        [Theory]
        [InlineData(2,true)]
        [InlineData(null, false)]
        public void IdValidationTest(int? id, bool result)
        {
            bool expected = validationService.isIdNotEqualsToNull(id);
            Assert.True(result == expected, "id validation success");
        }

        [Theory]
       // [InlineData(testU, true)]
        [InlineData(null, false)]
        public void userVlidationTest(User user, bool result)
        {
            throw new NotImplementedException("not finished");
            bool expected = validationService.isUserEqualsToNull(user);
            Assert.True(result == expected, "id validation success");
        }
    }
}
