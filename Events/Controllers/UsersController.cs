﻿using Events.Models.UserModels;
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
        private UserService userService = new UserService();
        private UserValidationService validationService = new UserValidationService();

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
            if (validationService.isIdNotEqualsToNull(id))
            {
                if (validationService.isUserEqualsToNull(userService.getUserById(id.Value)))
                    return Ok(userService.getUserById(id.Value));
                return NotFound(ErrorService.GetError("User not found"));
            }
            return NotFound(ErrorService.GetError("Id is wrong"));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult deleteUser(int? id)
        {
            User user = userService.getUserById(id.Value);
            if (validationService.isIdNotEqualsToNull(id) && validationService.isUserEqualsToNull(user))
            {
                userService.deleteUserById(id.Value, user);
                return NoContent();
            }
            return NotFound(ErrorService.GetError("User not found"));
        }

        [HttpPatch("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult banUser(int? id)
        {
            User user = userService.getUserById(id.Value);
            if (validationService.isIdNotEqualsToNull(id) && validationService.isUserEqualsToNull(user))
            {
                userService.BanOrUnban(user);
                return Ok(user);
            }
            return NotFound(ErrorService.GetError("User not found"));
        }
    }
}