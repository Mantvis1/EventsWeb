using System.Collections.Generic;
using System.Linq;
using Events.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class EventsController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<Event> events = new List<Event>();
        
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAll()
        {
            events = db.Events.ToList();
            if (events.Count > 0)
                return Ok(events);
            return NotFound(new Error("0 events was found"));
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult getEventById(int id)
        {
            Event e = db.Events.FirstOrDefault(x => x.id == id);
            if (e != null)
                return Ok(e);
            return NotFound(new Error("Event not found"));
        }

        [HttpDelete("{id}")]
        public ActionResult deleteEvent(int id)
        {
            Event @event = db.Events.FirstOrDefault(x => x.id == id);
            if (@event != null)
            {
                db.Events.Remove(@event);
                db.SaveChanges();
                return NoContent();
            }
            return NotFound(new Error("Events not found"));
        }

        [HttpPost("{title}/{summary}/{createdBy}")]
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