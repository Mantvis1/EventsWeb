using System.Collections.Generic;
using System.Linq;
using Events.Constants;
using Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<User> users = new List<User>();

        // GET api/users
        [HttpGet]
        public ActionResult<List<User>> GetAll()
        { 
            users = db.User.ToList();
            return users;
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int? id)
        {
           if(id != null && id.Value <= db.User.Max(e => e.Id)) {
                users = db.User.ToList();
                return users[id.Value-1];
            }
            RedirectToAction("GetAll");
            return null; // need to show error message
        }

        [HttpPut("{id}")]
        public ActionResult<string> putNewUser(int id)
        {
            db.User.Add(new User("name"+id, "password"+id, false, false));
            db.SaveChanges();
            return "a";
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> deleteUser(int? id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id.Value);
            if (id != null && user != null)
            {
                db.User.Remove(user);
                db.SaveChanges();
                return true;
            }
            // wrong id, user not found
            return false;
        }

        [HttpPatch("{id}")]
        public ActionResult<bool> banUser(int ?id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id.Value);
            if (id != null && user != null)
            {
                if (user.IsBanned == true)
                    user.IsBanned = Banned.userIsNotBaned();
                else
                    user.IsBanned = Banned.userIsBaned();
                db.SaveChanges();
                return true;
            }
                return false;
        }

    }
}