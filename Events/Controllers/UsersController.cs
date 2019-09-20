using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return new string[] { "users1", "users2" };
        }

        // GET users/5
        [HttpGet("{id}")]
        public ActionResult<string> GetById(int id)
        {
            return "users " + id;
        }
    }
}