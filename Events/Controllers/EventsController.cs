using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        // GET evets
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return new string[] { "event1", "event2" };
        }
    }
}