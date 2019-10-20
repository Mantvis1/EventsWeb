using Events.Services;
using EventsApiTest.TestData;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace EventsApiTest.TestServices
{
    [ExcludeFromCodeCoverage]
    public class EventServiceTest : IDisposable
    {
        private AfterAndBeforeTests tests;
        private TestConstants constants;
        private EventService eventService;

        public EventServiceTest()
        {
            tests = new AfterAndBeforeTests();
            eventService = new EventService();
            constants = new TestConstants();
        }

        public void Dispose()
        {
            tests.clearDB();
        }

        [Fact]
        public void CreateNewEventTest()
        {
            int count = eventService.getAllEventsCount() + 1;
            eventService.createNewEvent(
                constants.getTestEvent().title,
                constants.getTestEvent().summary,
                constants.getTestEvent().CreatedBy);
            Assert.Equal(count, eventService.getAllEventsCount());
        }

        [Fact]
        public void GetEventsCount()
        {
            var actuly = eventService.getAllEvents();
            Assert.NotEmpty(actuly);
            Assert.True(actuly.Count == constants.getEventCount());
        }
    }
}
