using System.Collections.Generic;
using System.Linq;
using Events.Constants;
using Events.Models;
using Events.Models.UserModels;
using Events.Services;
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
        private UserService userService = new UserService();

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAll()
        {
            if (userService.getListLength() > 0)
                return Ok(userService.getAllUsers());
            return NotFound(ErrorService.GetError("Users list is empty"));
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById(int? id)
        {
            if (id != null && id.Value <= db.User.Max(e => e.Id))
            {
                users = db.User.ToList();
                User user = users.FirstOrDefault(x => x.Id == id);
                if (user != null)
                    return Ok(user);
                return NotFound(new Error("User not found"));
            }
            return NotFound(new Error("Id is worng"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult deleteUser(int? id)
        {
            User user = db.User.FirstOrDefault(x => x.Id == id.Value);
            if (id != null && user != null)
            {
                List<UserEvents> userEvents = db.userEvents.Where(x => x.Participan == id.Value).ToList();
                if (userEvents.Count > 0)
                {
                    db.userEvents.RemoveRange(userEvents);
                    db.SaveChanges();
                }
                List<Event> events = db.Events.Where(x => x.CreatedBy == id.Value).ToList();
                if (userEvents.Count > 0)
                {
                    db.Events.RemoveRange(events);
                    db.SaveChanges();
                }
                List<Support> supports = db.Support.Where(x => x.SolvedBy == id.Value || x.WritenBy == id.Value).ToList();
                if (supports.Count > 0)
                {
                    db.Support.RemoveRange(supports);
                    db.SaveChanges();
                }
                db.User.Remove(user);
                db.SaveChanges();
                return NoContent();
            }
            return NotFound(new Error("User not found"));
        }

        [HttpPatch("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult banUser(int? id)
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