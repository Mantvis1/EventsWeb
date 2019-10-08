using System.Linq;
using Events.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Events.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();

        [HttpPost("{userName}/{password}")]
        public ActionResult login(string userName, string password)
        {
            User user = db.User.Where(x => x.Name == userName && x.Password == password).FirstOrDefault();
            if(user != null)
            {
                // login sucessful
            }
            return NotFound(new Error("user not found"));
        }

        [HttpPost("{userName}/{password}/{email}")]
        public ActionResult register(string userName, string password, string email)
        {
            if(userName != null && password != null && email != null)
            {
                User user = new User(userName, password, email, false, false);
                db.User.Add(user);
                return Created("", user);
            }
            return NotFound();
        }

        public static bool isLoggedIn()
        {
            return true;
        }
    }
}