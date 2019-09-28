using System.Collections.Generic;
using System.Linq;
using Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserEventsController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<UserEvents> userEvents = new List<UserEvents>();

        [HttpGet("userId")]
        public ActionResult<IEnumerable<UserEvents>> GetUserEvents(int userId)
        {
            return db.userEvents.Where(x => x.Participant == userId).ToList();

        }

        [HttpGet("eventId")]
        public ActionResult<int> GetEventParticipantsCount(int eventId)
        {
            return db.userEvents.Where(x => x.EventId == eventId).Count();
        }

        [HttpDelete("userId")]
        public ActionResult<bool> Delete(int userId)
        {
            return false;
        }

        
        public void DeleteParticipantsFromEvent(int userId)
        {
           // delete all users from specified event
        }
    }
}