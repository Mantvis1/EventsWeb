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

        [Theory]
        [InlineData("editedEventTitle", "EditedEventSummary",true,true)]
        [InlineData("", "",false,false)]
        [InlineData("editedEventTitle","", true,false)]
        [InlineData("", "EditedEventSummary", false,true)]
        public void EditEvent(string title, string summary, bool titleEdition, bool summaryEdition)
        {
            var @event = eventService.createNewEvent(constants.getTestEvent().title, constants.getTestEvent().summary, constants.getTestEvent().CreatedBy);
            int id = eventService.getEventIdByTitleAndAuthor(constants.getTestEvent().title, constants.getTestEvent().CreatedBy);

            eventService.editEventInformation(id, title, summary);

            Assert.Equal(eventService.getEventById(id).title != constants.getTestEvent().title, titleEdition);
            Assert.Equal(eventService.getEventById(id).summary != constants.getTestEvent().summary, summaryEdition);

            eventService.editEventInformation(id, constants.getTestEvent().title, constants.getTestEvent().summary);
        }
    }
}
