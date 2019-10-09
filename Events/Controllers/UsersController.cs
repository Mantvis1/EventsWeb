using System.Collections.Generic;
using System.Linq;
using Events.Constants;
using Events.Models;
using Events.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    // /api/users
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class UsersController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<User> users = new List<User>();

        [HttpGet]
        [Authorize]
        public ActionResult GetAll()
        { 
            users = db.User.ToList();
            if (users.Count > 0)
                return Ok(users);
            return NoContent();
        }

        [HttpGet("{id}")]
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
            User user = new User(name, password,"email@kate.com", false, false);
            db.User.Add(user);
            db.SaveChanges();
            return Created("", user);
        }

        [HttpDelete("{id}")]
        public ActionResult deleteUser(int? id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id.Value);
            if (id != null && user != null)
            {
                db.User.Remove(user);
                db.SaveChanges();
                return NoContent();
            }
            return NotFound(new Error("User not found"));
        }

        [HttpPatch("{id}")]
        public ActionResult banUser(int ?id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id.Value);
            if (id != null && user != null)
            {
                if (user.IsBanned == true)
                    user.IsBanned = Banned.unbanUser();
                else
                    user.IsBanned = Banned.banUser();
                db.SaveChanges();
                return Ok(user);
            }
            return NotFound(new Error("User not found"));
        }

    }
}