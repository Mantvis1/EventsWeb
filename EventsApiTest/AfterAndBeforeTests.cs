using Events.Services;
using EventsApiTest.TestData;
using System.Diagnostics.CodeAnalysis;

namespace EventsApiTest
{
    [ExcludeFromCodeCoverage]
    class AfterAndBeforeTests
    {
        private TestConstants testConstants = new TestConstants();

        private UserService userService = new UserService();
        private EventService eventService = new EventService();
        private SupportService supportService = new SupportService();

        public void clearDB()
        {
            while (userService.getUserByNameAndPassword(testConstants.getUser().Name, testConstants.getUser().Password) != null)
            {
                userService.deleteUserById(userService.getIdByNameAndPassword(new string[] { testConstants.getUser().Name, testConstants.getUser().Password }));
            }

            while(eventService.getEventByTitleAndAuthor(testConstants.getTestEvent().title, testConstants.getTestEvent().CreatedBy)!= null)
            {
                eventService.deleteEvent(eventService.getEventIdByTitleAndAuthor(testConstants.getTestEvent().title, testConstants.getTestEvent().CreatedBy));
            }
        }
    }
}
