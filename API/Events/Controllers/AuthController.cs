﻿using Events.Models;
using Events.Models.UserModels;
using Events.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;

namespace Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private ValidationService validationService = new ValidationService();
        private AuthService authService = new AuthService();
        private UserService userService = new UserService();

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult login(string userName, string password)
        {
            var header = Request.Headers["Authorization"];

            if (validationService.startsWithValidation(header, "Basic"))
            {
                var userNameAndPassword = authService.getNameAndPassword(header.ToString());

                if (validationService.objectValidation(userService.getUserByNameAndPassword(userNameAndPassword[0], userNameAndPassword[1])))
                { 
                    return Ok(authService.getToken(userNameAndPassword[0]));
                }
                return NotFound(ErrorService.GetError("user not found"));
            }
            return BadRequest(ErrorService.GetError("Something wrong with header"));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Register([FromBody]UserRegisterModel userRegisterModel)
        {
            if (validationService.textValidation(userRegisterModel.Name)
                && validationService.textValidation(userRegisterModel.Password)
                && validationService.emailValidation(userRegisterModel.Email))
            {
                return Ok(authService.createNewUser(userRegisterModel.Name, userRegisterModel.Password));
            }
            return NotFound(ErrorService.GetError("Username, password or email is not valid"));
        }
    }
}