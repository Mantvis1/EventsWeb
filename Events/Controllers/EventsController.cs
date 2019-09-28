using System.Collections.Generic;
using System.Linq;
using Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<Event> users = new List<Event>();
        
        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetAll()
        {
            return db.Events.ToList();
        }

        [HttpDelete("id")]
        public ActionResult<bool> deleteEvent(int id)
        {
            Event @event = db.Events.FirstOrDefault(x => x.id == id);
            if(@event != null)
            {
                db.Events.Remove(@event);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpPost]
        public ActionResult<bool> createNewEvent()
        {
            db.Events.Add(new Event("labai geras pavadinimas", "labai geras aprasymas", 0, 0));
            db.SaveChanges();
            return true;
        }

        [HttpPut("id")]
        public ActionResult<bool> edtiEventInformation(int id)
        {
            Event @event = db.Events.FirstOrDefault(x => x.id == id);
            if(@event != null)
            {
                @event.summary = "tiesiog aprasymas";
                @event.title = "tiesiog pavadinimas";
                db.SaveChanges();
            }
            return false;
        }

        [HttpGet("id")]
        public ActionResult<Event> getEvent(int id)
        {
            Event @event = db.Events.FirstOrDefault(x => x.id == id);
            return @event != null ? null : @event;        
        }
    }
}