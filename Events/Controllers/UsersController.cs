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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<User> users = new List<User>();

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAll()
        { 
            users = db.User.ToList();
            if (users.Count > 0)
                return Ok(users);
            return NotFound(new Error("Users list is empty"));
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById(int? id)
        {
           if(id != null && id.Value <= db.User.Max(e => e.Id)) {
                users = db.User.ToList();
                User user = users.FirstOrDefault(x => x.Id == id);
                if(user != null)    
                    return Ok(user);
                return NotFound(new Error("User not found"));
            }
            return NotFound(new Error("Id is worng"));
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