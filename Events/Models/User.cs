﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Models
{
    [Table("User")]
    public class User
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsBanned { get; set; }
        public bool IsAdmin { get; set; }

        public User(string name, string password,string email, bool isBanned, bool isAdmin)
        {
            Name = name;
            Password = password;
            Email = email;
            IsBanned = isBanned;
            IsAdmin = isAdmin;
        }
    }
}
