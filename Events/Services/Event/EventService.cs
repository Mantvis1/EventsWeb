﻿using Events.Models;
using System.Collections.Generic;
using System.Linq;

namespace Events.Services
{
    public class EventService
    {
        private EventsDBContext db = new EventsDBContext();

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
    }
}