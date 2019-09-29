using System.Collections.Generic;
using System.Linq;
using System.Net;
using Events.Constants;
using Events.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    // /api/users
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<User> users = new List<User>();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult GetAll()
        { 
            users = db.User.ToList();
            if (users.Count > 0)
                return Ok(users);
            return NoContent();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById(int? id)
        {
           if(id != null && id.Value <= db.User.Max(e => e.Id)) {
                users = db.User.ToList();
                return Ok(users.FirstOrDefault(x => x.Id == id));
            }
            return NotFound(new Error("User not found"));
        }

        [HttpPut("{name}/{password}")]
        public ActionResult<string> putNewUser(string name, string password)
        {
            db.User.Add(new User(name,password, false, false));
            db.SaveChanges();
            return "new user "+name+" was added";
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult deleteUser(int? id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id.Value);
            if (id != null && user != null)
            {
                db.User.Remove(user);
                db.SaveChanges();
                return Ok(user);
            }
            return NotFound(new Error("User not found"));
        }

        [HttpPatch("{id}")]
        public ActionResult<bool> banUser(int ?id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id.Value);
            if (id != null && user != null)
            {
                if (user.IsBanned == true)
                    user.IsBanned = Banned.unbanUser();
                else
                    user.IsBanned = Banned.banUser();
                db.SaveChanges();
                return true;
            }
                return false;
        }

    }
}