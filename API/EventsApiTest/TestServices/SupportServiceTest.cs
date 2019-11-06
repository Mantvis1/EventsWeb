using Events.Services;
using EventsApiTest.TestData;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace EventsApiTest.TestServices 
{
    [ExcludeFromCodeCoverage]
    public class SupportServiceTest : IDisposable
    {
        private AfterAndBeforeTests tests;
        private TestConstants constants;
        private SupportService supportService; 

        public SupportServiceTest()
        {
            tests = new AfterAndBeforeTests();
            constants = new TestConstants();
            supportService = new SupportService();
        }

        public void Dispose()
        {
            tests.clearDB();
        }

        [Fact]
        public void GetSupportsCount()
        {
            var actuly = supportService.getAllSuports();
            Assert.NotEmpty(actuly);
            Assert.True(actuly.Count == constants.getSupportCount());
        }

        [Fact]
        public void AddAndDeleteSupport()
        {
            supportService.AddSupportToDatabase(constants.getSupport());
            int id = supportService.GetSupportByTitleAndAuthor(constants.getSupport().Title, constants.getSupport().WritenBy);

            Assert.NotNull(supportService.getSupportById(id));

            supportService.deleteSupportFromDatabase(id);

            Assert.Null(supportService.getSupportById(id));
        }
    }
}
