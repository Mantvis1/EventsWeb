using System.ComponentModel.DataAnnotations;

namespace Events.Models.UserModels
{
    public class UserLogin
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public UserLogin(string name, string password)
        {
            Name = name;
            Password = password;
        }


    }
}
