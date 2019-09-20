using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        // GET complaints
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            return new string[] { "complaint1", "complaint2" };
        }
    }
}