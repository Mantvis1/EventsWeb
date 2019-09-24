using System.Collections.Generic;
using System.Linq;
using Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SupportsController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<Support> support = new List<Support>();

        // GET supports
        [HttpGet]
        public ActionResult<IEnumerable<Support>> GetAll()
        {
            support = db.Support.ToList();
            return support;
        }

        [HttpGet("{id}")]
        public ActionResult<Support> GetById(int? id)
        {
            if (id != null && id.Value <= db.User.Max(e => e.Id))
            {
                support = support = db.Support.ToList();
                return support[id.Value - 1];
            }
            RedirectToAction("GetAll");
            return null; // need to show error message
        }

        [HttpPost]
        public ActionResult<bool> createSupportMessage()
        {
            db.Support.Add(new Support("title", "text text some text", 3,"yre",2));
            db.SaveChanges();
            return true;
        }
    }
}