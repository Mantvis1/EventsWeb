using Events.Constants;
using Events.Models;
using Events.Models.UserModels;
using System.Collections.Generic;
using System.Linq;

namespace Events.Services
{
    public class UserService
    {
        private EventsDBContext db = new EventsDBContext();
        private UserEventsService userEventsService = new UserEventsService();
        private EventService eventService = new EventService();
        private SupportService supportService = new SupportService();

        public List<User> getAllUsers()
        {
            return db.User.ToList();
        }

        public int getListLength()
        {
            return db.User.ToList().Count;
        }

        public User getUserById(int id)
        {
            return db.User.FirstOrDefault(x => x.Id == id);
        }

        public void BanOrUnban(User user)
        {
            if (user.IsBanned == true)
                user.IsBanned = Banned.unbanUser();
            else
                user.IsBanned = Banned.banUser();
            db.SaveChanges();
        }

        public void deleteUserById(int id, User user)
        {
            if (userEventsService.getUserEventsByParticipanIdCount(id) > 0)
            {
                db.userEvents.RemoveRange(userEventsService.getUserEventsByParticipanId(id));
                db.SaveChanges();
            }

            if (eventService.getEventsListByCreatorIdCount(id) > 0)
            {
                db.Events.RemoveRange(eventService.getEventsListByCreatorId(id));
                db.SaveChanges();
            }

            if (supportService.getSupportListLength(id) > 0)
            {
                db.Support.RemoveRange(supportService.getSupportList(id));
                db.SaveChanges();
            }

            db.User.Remove(user);
            db.SaveChanges();
        }
    }
}
