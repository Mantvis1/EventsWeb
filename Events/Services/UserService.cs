using Events.Models;
using Events.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Events.Services
{
    public class UserService
    {
        private EventsDBContext db = null;
        private List<User> users = null;

        public UserService()
        {
            db = new EventsDBContext();
            users = db.User.ToList();
        }

        public List<User> getAllUsers()
        {
            return users;
        }

        public int getListLength()
        {
            return db.User.ToList().Count;
        }

        public User getUserById(int id)
        {
            return users[id];
        }

    }
}
