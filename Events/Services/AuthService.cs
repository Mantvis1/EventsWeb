using Events.Models;
using Events.Models.UserModels;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using EventsApiTest;

namespace Events.Services
{
    public class AuthService
    {
        private EventsDBContext db = new EventsDBContext();

        public User createNewUser(string name, string password)
        {
            User user = new User(name,password, false, false);
            db.User.Add(user);
            db.SaveChanges();
            return user;
        }

        public string getToken(string userNameAndPassword)
        {
            var claimsData = new[] { new Claim(ClaimTypes.Name, userNameAndPassword) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("45wgr6dvh634g1kj684hr894v4sg9dgh54vsd4bdf4bs4d9n5cvs4bdfv4z"));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "mysite.com",
                audience: "mysite.com",
                expires: DateTime.Now.AddHours(1),
                claims: claimsData,
                signingCredentials: signIn
                );

            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        public string[] getNameAndPassword(string header)
        {
            var credValue = header.ToString().Substring("Basic".Length).Trim();
            var userNameAndPasswordenc = Encoding.UTF8.GetString(Convert.FromBase64String(credValue));
            return userNameAndPasswordenc.Split(":");
        }

        public virtual bool createNewUser(IUser user)
        {
            return true;
        }

    }
}
