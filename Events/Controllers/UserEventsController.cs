using System.Collections.Generic;
using System.Linq;
using Events.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    // /api/userEvents
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserEventsController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<UserEvents> userEvents = new List<UserEvents>();

        [HttpGet("{userId}")]
        public ActionResult getUserEvents(int? userId)
        {
            if (userId != null)
            {
                List<UserEvents> userEvents = db.userEvents.Where(x => x.Participan == userId.Value).ToList();
                if (userEvents.Count > 0)
                {
                    return Ok(userEvents);
                }
                return NoContent();
            }
            return NotFound(new Error("user id not found"));
        }

        [HttpDelete("{userId}/{eventId}")]
        public ActionResult deleteUserEvent(int? userId, int? eventId)
        {
            if (userId != null && eventId != null)
            {
                UserEvents userEvent = db.userEvents.Where(x => x.Participan == userId.Value && x.EventId == eventId.Value).FirstOrDefault();
                if (userEvent != null)
                {
                    db.userEvents.Remove(userEvent);
                    db.SaveChanges();
                    return NoContent();
                }
                return NoContent();
            }
            return NotFound(new Error("user or event id not found"));
        }

        [HttpPost("{userId}/{eventId}")]
        public ActionResult joinEvent(int? userId, int? eventId)
        {
            if (userId != null && eventId != null)
            {
                if (db.User.FirstOrDefault(x => x.Id == userId.Value) != null && db.Events.FirstOrDefault(x => x.id == eventId.Value) != null)
                {
                    UserEvents userEvents = new UserEvents(userId.Value, eventId.Value);
                    db.userEvents.Add(userEvents);
                    db.SaveChanges();
                    return Ok(userEvents);
                }
                return NotFound(new Error("user or event id not found"));
            }
            return NotFound(new Error("user or event id not found"));
        }
    }
}