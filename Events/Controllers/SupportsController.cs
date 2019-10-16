using System.Collections.Generic;
using System.Linq;
using Events.Models;
using Events.Services;
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
        private SupportService supportService = new SupportService();
        private ValidationService validationService = new ValidationService();

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAll()
        {
            if (supportService.getAllSuportsCount() > 0)
                return Ok(supportService.getAllSuports());
            return NotFound(ErrorService.GetError("Support message list not found"));
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetById(int? id)
        {
            if (validationService.idValdation(id))
            {
                if (validationService.objectValidation(supportService.getSupportById(id.Value)))
                {
                    return Ok(supportService.getSupportById(id.Value));
                }
                return NotFound(ErrorService.GetError("Support not found"));
            }
            return NotFound(ErrorService.GetError("Support id not found"));
        }

        [HttpPost("new")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult createSupportMessage([FromBody]Support support)
        {

            if (validationService.idValdation(support.WritenBy) 
                && validationService.textValidation(support.Title) 
                && validationService.textValidation(support.Summary))
            {
                if (validationService.objectValidation(support))
                {
                    supportService.AddSupportToDatabase(support);
                    return Created("", support);
                }
                return NotFound(ErrorService.GetError("User can not be found"));
            }
            return NotFound(ErrorService.GetError("UserId, Title and summary can not be empty"));
        }

        [HttpPatch("solve/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult writeSolution(int? id, string message, int? solvedBy)
        {
            if (validationService.objectValidation(supportService.getSupportById(id.Value))
                && validationService.textValidation(message) && validationService.idValdation(solvedBy))
            {
                supportService.getSupportById(id.Value).SolvedBy = solvedBy.Value;
                supportService.getSupportById(id.Value).Solution = message;
                db.SaveChanges();
                return Ok(supportService.getSupportById(id.Value));
            }
            return NotFound(ErrorService.GetError("id, message and solved by can not be empty"));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult deleteSupportMessage(int? id)
        {
            if (validationService.objectValidation(supportService.getSupportById(id.Value)))
            {
                db.Support.Remove(supportService.getSupportById(id.Value));
                db.SaveChanges();
                return NoContent();
            }
            return NotFound(ErrorService.GetError("Id not null or dont exists"));
        }
    }
}
