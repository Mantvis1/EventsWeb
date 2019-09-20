using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Models
{
    [Table("User")]
    public class User
    {
        [Required]
        public int id { get; set; }
        public string name { get; set; }

    }
}
