using System.Collections.Generic;
using System.Linq;
using Events.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportsController : ControllerBase
    {
        private EventsDBContext db = new EventsDBContext();
        private List<Support> supports = new List<Support>();

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAll()
        {
            supports = db.Support.ToList();
            if (supports.Count > 0)
                return Ok(supports);
            return NotFound(new Error("Support message list not found"));
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById(int? id)
        {
            if (id != null && id.Value <= db.User.Max(e => e.Id))
            {
                supports = db.Support.ToList();
                Support support = supports.FirstOrDefault(x => x.Id == id);
                if (support != null)
                {
                    return Ok(support);
                }
                return NotFound(new Error("Support not found"));
            }
            return NotFound(new Error("Support id not found"));
        }

        [HttpPost("{writenBy}/{title}/{summary}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult createSupportMessage(int? writenBy, string title, string summary)
        {
            if (writenBy != null && title != null && summary != null)
            {
                if (db.User.FirstOrDefault(x => x.Id == writenBy.Value) != null)
                {
                    Support support = new Support(title, summary, writenBy.Value);
                    db.Support.Add(support);
                    db.SaveChanges();
                    return Created("", support);
                }
                return NotFound(new Error("User can not be found"));
            }
            return NotFound(new Error("UserId, Title and summary can not be empty"));
        }

        [HttpPatch("{id}/{solvedBy}/{message}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> writeSolution(int id, string message, int? solvedBy)
        {
            Support support = db.Support.FirstOrDefault(x => x.Id == id);
            if (support != null && message != null && solvedBy != null)
            {
                support.SolvedBy = solvedBy.Value;
                support.Solution = message;
                db.SaveChanges();
                return Ok(support);
            }
            return NotFound(new Error("id, message and solved by can not be empty"));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> deleteSupportMessage(int? id)
        {
            Support support = db.Support.FirstOrDefault(x => x.Id == id);
            if (support != null)
            {
                db.Support.Remove(support);
                db.SaveChanges();
                return NoContent();
            }
            return NotFound(new Error("Id not null or dont exists"));
        }
    }
}
