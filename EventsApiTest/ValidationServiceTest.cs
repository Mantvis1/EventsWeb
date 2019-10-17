using Events.Models.UserModels;
using Events.Services;
using System;
using Xunit;

namespace EventsApiTest
{
    public class ValidationServiceTest
    {
        private ValidationService validationService = new ValidationService();
        private User testUser = new User("name", "pass", false, true);

        [Theory]
        [InlineData(2,true)]
        [InlineData(null, false)]
        public void IdValidationTest(int? id, bool result)
        {
            bool expected = validationService.idValdation(id);
            Assert.True(result == expected, "id validation success");
        }

        [Fact]
        public void userVlidationTest()
        {
            throw new NotImplementedException("need to finish this test");
        }

        [Theory]
        [InlineData("some text", true)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void textValidationTest(string text, bool result)
        {
            Assert.True(validationService.textValidation(text) == result, "text validation success");
        }

        [Theory]
        [InlineData("sometext@gmail.com", true)]
        [InlineData(null, false)]
        public void emailValidationTest(string email, bool result)
        {
            Assert.True(validationService.emailValidation(email) == result, "email validation success");
        }

        [Theory]
        [InlineData("testsometext@gmail.com", true)]
        [InlineData(null, false)]
        [InlineData("sometext", false)]
        public void startsWithValidationTest(string text, bool result)
        {
            Assert.True(validationService.startsWithValidation(text,"test") == result, "starts with validation success");
        }
    }
}
