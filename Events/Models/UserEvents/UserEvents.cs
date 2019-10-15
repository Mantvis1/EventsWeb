using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Models
{
    [Table("UserEvents")]
    public class UserEvents
    {
        [Required]
        public int Id { get; set; }
        public int Participan { get; set; }
        public int EventId { get; set; }

        public UserEvents(int participan, int eventId)
        {
            Participan = participan;
            EventId = eventId;
        }   
    }
}