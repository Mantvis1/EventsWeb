using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Models
{
    [Table("Event")]
    public class Event
    {
        [Required]
        public int id { get; set; }
        public string title { get; set; }
        public string summary { get; set; }
    }
}
