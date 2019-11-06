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

        public UserEvents getEventByUserIdAndEventId(int userId, int eventId)
        {
            return db.userEvents.Where(x => x.Participan == userId && x.EventId == eventId).FirstOrDefault();
        }

        public void deleteUserEvent(int userId, int eventId)
        {
            db.userEvents.Remove(getEventByUserIdAndEventId(userId, eventId));
            db.SaveChanges();
        }

        public UserEvents joinUserToEvent(int userId, int eventId)
        {
            UserEvents userEvents = new UserEvents(userId, eventId);
            db.userEvents.Add(userEvents);
            db.SaveChanges();
            return userEvents;
        }
    }
}
