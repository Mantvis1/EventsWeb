using Events.Models;
using Events.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Events.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    { 
        private EventsDBContext db = new EventsDBContext();

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult login(string userName, string password)
        {
            var header = Request.Headers["Authorization"];

            if (header.ToString().StartsWith("Basic"))
            {
                var credValue = header.ToString().Substring("Basic".Length).Trim();
                var userNameAndPasswordenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue));
                var userNameAndPassword = userNameAndPasswordenc.Split(":");

                User user = db.User.Where(x => x.Name == userNameAndPassword[0] && x.Password == userNameAndPassword[1]).FirstOrDefault();
                if (user != null)
                {
                    var claimsData = new[] { new Claim(ClaimTypes.Name, userNameAndPassword[0]) };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("45wgr6dvh634g1kj684hr894v4sg9dgh54vsd4bdf4bs4d9n5cvs4bdfv4z"));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        issuer: "mysite.com",
                        audience: "mysite.com",
                        expires: DateTime.Now.AddHours(1),
                        claims: claimsData,
                        signingCredentials: signIn
                        );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(tokenString);
                }
                return NotFound(new Error("user not found"));
            }
            return BadRequest("Something wrong with header");
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Register([FromBody]UserRegisterModel userRegisterModel)
        {
            if (userRegisterModel.Name != null && userRegisterModel.Password != null && userRegisterModel.Email != null)
            {
                User user = new User(userRegisterModel.Name, userRegisterModel.Password, false, false);
                db.User.Add(user);
                db.SaveChanges();
                return Ok(user);
            }
            return NotFound();
        }
    }
}