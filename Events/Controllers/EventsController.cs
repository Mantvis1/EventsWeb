﻿using Events.Models;
using Events.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EventsController : ControllerBase
    {
        private EventService eventsService = new EventService();
        private EventValidationService eventValidation = new EventValidationService();
        private UserService userService = new UserService();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetAll()
        {
            if (eventsService.getAllEventsCount() > 0)
                return Ok(eventsService.getAllEvents());
            return NotFound(ErrorService.GetError("0 events was found"));
        }

        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult getEventById(int? id)
        {
            if (eventValidation.isIdNotEqualsToNull(id))
            {
                if (eventValidation.isUserEqualsToNull(eventsService.getEventById(id.Value)))
                    return Ok(eventsService.getEventById(id.Value));
                return NotFound(ErrorService.GetError("Event not found"));
            }
            return NotFound(ErrorService.GetError("Wrong id"));
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult deleteEvent(int id)
        {
            if (eventsService.getEventById(id) != null)
            {
                eventsService.deleteEvent(id);
                return NoContent();
            }
            return NotFound(ErrorService.GetError("Event not found"));
        }

        [HttpPost("new")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult createNewEvent([FromBody]Event @event)
        {
            if (eventValidation.newEventCreation(@event.title, @event.summary, @event.CreatedBy))
            {
                if (userService.getUserById(@event.CreatedBy) != null)
                {
                    return Ok(eventsService.createNewEvent(@event.title, @event.summary, @event.CreatedBy));
                }
                return NotFound(ErrorService.GetError("user does not exist"));
            }
            return NotFound(ErrorService.GetError("title summary and createdBy can not be empty"));
        }

        [HttpPut("edit/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult edtiEventInformation(int? id,[FromBody]EventUpdateFrom @event)
        {
            if (eventsService.getEventById(id.Value) != null)
            {
                return Ok(eventsService.editEventInformation(id.Value, @event.title, @event.summary));
            }
            return NotFound(ErrorService.GetError("event is not found"));
        }
    }
}