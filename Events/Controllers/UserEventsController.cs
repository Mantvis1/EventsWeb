using Events.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEventsController : ControllerBase
    {
        private ValidationService validationService = new ValidationService();
        private UserEventsService userEventsService = new UserEventsService();
        private UserService userService = new UserService();
        private EventService eventService = new EventService();

        [HttpGet("{userId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult getUserEvents(int? userId)
        {
            if (validationService.idValdation(userId))
            {
                if (userEventsService.getUserEventsByParticipanIdCount(userId.Value) > 0)
                {
                    return Ok(userEventsService.getUserEventsByParticipanId(userId.Value));
                }
                return NotFound(ErrorService.GetError("0 events were found"));
            }
            return NotFound(ErrorService.GetError("user id not found"));
        }

        [HttpDelete("{userId}/{eventId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult deleteUserEvent(int? userId, int? eventId)
        {
            if (validationService.idValdation(userId) && validationService.idValdation(eventId))
            {
                if (validationService.objectValidation(userEventsService.getEventByUserIdAndEventId(userId.Value, eventId.Value)))
                {
                    userEventsService.deleteUserEvent(userId.Value, eventId.Value);
                    return NoContent();
                }
                return NotFound(ErrorService.GetError("event not found"));
            }
            return NotFound(ErrorService.GetError("user or event id not found"));
        }

        [HttpPost("{userId}/{eventId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult joinEvent(int? userId, int? eventId)
        {
            if (validationService.idValdation(userId) && validationService.idValdation(eventId))
            {
                if (validationService.objectValidation(userService.getUserById(userId.Value))
                    && validationService.objectValidation(eventService.getEventById(eventId.Value)))
                {   
                    return Ok(userEventsService.joinUserToEvent(userId.Value, eventId.Value));
                }
                return NotFound(ErrorService.GetError("user or event id not found"));
            }
            return NotFound(ErrorService.GetError("user or event id not found"));
        }
    }
}