using Events.Models;
using System.Collections.Generic;
using System.Linq;

namespace Events.Services
{
    public class EventService
    {
        private EventsDBContext db = new EventsDBContext();
        private UserEventsService userEventsService = new UserEventsService();
        private EventValidationService validationService = new EventValidationService();

        public List<Event> getEventsListByCreatorId(int id)
        {
            return db.Events.Where(x => x.CreatedBy == id).ToList();
        }

        public int getEventsListByCreatorIdCount(int id)
        {
            return db.Events.Where(x => x.CreatedBy == id).ToList().Count();
        }

        public List<Event> getAllEvents()
        {
            return db.Events.ToList();
        }

        public int getAllEventsCount()
        {
            return db.Events.ToList().Count();
        }

        public Event getEventById(int id)
        {
            return db.Events.FirstOrDefault(x => x.id == id);
        }

        public void deleteEvent(int id)
        {
            List<UserEvents> userEvents = userEventsService.getUserEventsByEventId(id);
            if (userEvents.Count > 0)
            {
                db.userEvents.RemoveRange(userEvents);
                db.SaveChanges();
            }
            db.Events.Remove(getEventById(id));
            db.SaveChanges();
        }

        public Event createNewEvent(string title, string summary, int createdBy)
        {
            Event @event = new Event(title, summary, createdBy);
            db.Events.Add(@event);
            db.SaveChanges();
            return @event;
        }

        public Event editEventInformation(int id, string title, string summary)
        {
            Event @event = getEventById(id);
            if (validationService.textFieldValidation(title))
                @event.title = title;
            if (validationService.textFieldValidation(summary))
                @event.summary = summary;
            db.SaveChanges();
            return @event;
        }
    }
}
