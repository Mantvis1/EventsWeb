using System.Collections.Generic;
using System.Linq;
using Events.Models;
using Events.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EventsController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<Event> events = new List<Event>();
        private EventService eventsService = new EventService();
        private EventValidationService eventValidation = new EventValidationService();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAll()
        {

            if (eventsService.getAllEventsCount() > 0)
                return Ok(eventsService.getAllEvents());
            return NotFound(ErrorService.GetError("0 events was found"));
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult getEventById(int? id)
        {
            if (eventValidation.isIdNotEqualsToNull(id))
            {
                if (eventValidation.isUserEqualsToNull(eventsService.getEventById(id.Value)))
                    return Ok(eventsService.getEventById(id.Value));
                return NotFound(new Error("Event not found"));
            }
            return NotFound(ErrorService.GetError("Wrong id"));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult deleteEvent(int id)
        {
            Event @event = eventsService.getEventById(id);
            if (@event != null)
            {
                List<UserEvents> userEvents = db.userEvents.Where(x => x.EventId == id).ToList();
                if (userEvents.Count > 0)
                {
                    db.userEvents.RemoveRange(userEvents);
                    db.SaveChanges();
                }
                db.Events.Remove(@event);
                db.SaveChanges();
                return NoContent();
            }
            return NotFound(new Error("Event not found"));
        }

        [HttpPost("{title}/{summary}/{createdBy}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult createNewEvent(string title, string summary, int? createdBy)
        {
            if (title != null && summary != null && createdBy != null)
            {
                if (db.User.FirstOrDefault(x => x.Id == createdBy.Value) != null)
                {
                    Event @event = new Event(title, summary, createdBy.Value);
                    db.Events.Add(@event);
                    db.SaveChanges();
                    return Ok(@event);
                }
                return NotFound(new Error("user does not exist"));
            }
            return NotFound(new Error("title summary and createdBy can not be empty"));
        }

        [HttpPut("{id}/{title}/{summary}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult edtiEventInformation(int id, string title, string summary)
        {
            Event @event = db.Events.FirstOrDefault(x => x.id == id);
            if (@event != null)
            {
                if (title != null)
                    @event.title = title;
                if (summary != null)
                    @event.summary = summary;
                db.SaveChanges();
                return Ok(@event);
            }
            return NotFound(new Error("event is not found"));
        }
    }
}