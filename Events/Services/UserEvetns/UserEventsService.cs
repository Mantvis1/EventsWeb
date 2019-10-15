using Events.Models;
using System.Collections.Generic;
using System.Linq;

namespace Events.Services
{
    public class UserEventsService
    {
        private EventsDBContext db = new EventsDBContext();

        public List<UserEvents> getUserEventsByParticipanId(int id)
        {
            return db.userEvents.Where(x => x.Participan == id).ToList();
        }

        public int getUserEventsByParticipanIdCount(int id)
        {
            return db.userEvents.Where(x => x.Participan == id).ToList().Count;
        }

        public List<UserEvents> getUserEventsByEventId(int id)
        {
            return db.userEvents.Where(x => x.EventId == id).ToList();
        }

        public int getUserEventsByEventIdCount(int id)
        {
            return db.userEvents.Where(x => x.EventId == id).ToList().Count;
        }
    }
}
