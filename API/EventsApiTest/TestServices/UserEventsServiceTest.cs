using Events.Services;
using EventsApiTest.TestData;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace EventsApiTest.TestServices
{
    [ExcludeFromCodeCoverage]
    public class UserEventsServiceTest : IDisposable
    {
        private AfterAndBeforeTests tests;
        private UserService userService;
        private AuthService authService;
        private TestConstants constants;
        private EventService eventService;
        private UserEventsService userEventsService;
        public UserEventsServiceTest()
        {
            tests = new AfterAndBeforeTests();
            userService = new UserService();
            authService = new AuthService();
            constants = new TestConstants();
            eventService = new EventService();
            userEventsService = new UserEventsService();
    }

        public void Dispose()
        {
            tests.clearDB();
        }

        [Fact]
        public void UserJoinToEventTest()
        {
            authService.createNewUser(constants.getUser().Name, constants.getUser().Password);
            int userId = userService.getIdByNameAndPassword(constants.getNameAndPass());

            eventService.createNewEvent(constants.getTestEvent().title, constants.getTestEvent().summary, constants.getTestEvent().CreatedBy);
            int eventId = eventService.getEventIdByTitleAndAuthor(constants.getTestEvent().title, constants.getTestEvent().CreatedBy);

            userEventsService.joinUserToEvent(userId, eventId);

            Assert.NotNull(userEventsService.getEventByUserIdAndEventId(userId, eventId));

            userEventsService.deleteUserEvent(userId, eventId);

            Assert.Null(userEventsService.getEventByUserIdAndEventId(userId, eventId));
        }
    }
}
