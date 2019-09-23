using System;
using System.Collections.Generic;
using System.Linq;
using Events.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetById(int? id)
        {
           if(id != null && id.Value <= db.User.Max(e => e.id)) {
                users = db.User.ToList();
                return users[id.Value-1];
            }
            RedirectToAction("GetAll");
            return null; // need to show error message
        }

        [HttpPut("{id}")]
        public ActionResult<string> putNewUser(int id)
        {
            db.User.Add(new User {name = "name " + id });
            db.SaveChanges();
            return "a";
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> deleteUser(int? id)
        {
            User user = db.User.FirstOrDefault(x => x.id == id.Value);
            if (id != null && user != null)
            {
                db.User.Remove(user);
                db.SaveChanges();
                return true;
            }
            // wrong id, user not found
            return false;
        }

    }
}