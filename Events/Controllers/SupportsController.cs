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
        private SupportValidationService validationService = new SupportValidationService();

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
            if (validationService.isIdEqualToNull(id))
            {
                if (validationService.ifObejectIsNull(supportService.getSupportById(id.Value)))
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

            if (validationService.isSupportFromFilled(support.WritenBy,support.Title,support.Summary))
            {
                if (validationService.ifObejectIsNull(support))
                {
                    supportService.AddSupportToDatabase(support);
                    return Created("", support);
                }
                return NotFound(ErrorService.GetError("User can not be found"));
            }
            return NotFound(ErrorService.GetError("UserId, Title and summary can not be empty"));
        }

        // need to finish it
        [HttpPatch("{id}/{solvedBy}/{message}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> writeSolution(int? id, string message, int? solvedBy)
        {
            if (validationService.ifObejectIsNull(supportService.getSupportById(id.Value))
                && validationService.textFieldValidation(message) && validationService.creatorValidation(solvedBy))
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
            if (validationService.ifObejectIsNull(supportService.getSupportById(id.Value)))
            {
                db.Support.Remove(supportService.getSupportById(id.Value));
                db.SaveChanges();
                return NoContent();
            }
            return NotFound(ErrorService.GetError("Id not null or dont exists"));
        }
    }
}
