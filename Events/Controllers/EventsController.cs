using System.Collections.Generic;
using System.Linq;
using Events.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class EventsController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<Event> events = new List<Event>();

        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetAll()
        {
            events = db.Events.ToList();
            if (events.Count > 0)
                return Ok(events);
            return NoContent();
        }

        [HttpDelete("id")]
        public ActionResult<bool> deleteEvent(int id)
        {
            Event @event = db.Events.FirstOrDefault(x => x.id == id);
            if (@event != null)
            {
                db.Events.Remove(@event);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpPost("{title}/{summary}")]
        public ActionResult createNewEvent(string title, string summary)
        {
            if (title != null && summary != null)
            {
                Event @event = new Event(title, summary);
                db.Events.Add(@event);
                db.SaveChanges();
                return Ok(@event);
            }
            return NotFound(new Error("title and summary can not be empty"));
        }

        [HttpPut("{id}/{title}/{summary}")]
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

        [HttpGet("id")]
        public ActionResult getEvent(int id)
        {
            Event @event = db.Events.FirstOrDefault(x => x.id == id);
            if (@event != null)
                return Ok(@event);
            return NotFound(new Error("Event not found"));
        }
    }
}