using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Models
{
    [Table("UserEvents")]
    public class UserEvents
    {
        [Required]
        public int Id { get; set; }
        public int Participant { get; set; }
        public int EventId { get; set; }

        public UserEvents(int participant, int eventId)
        {
            Participant = participant;
            EventId = eventId;
        }   
    }
}