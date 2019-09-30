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
        public int CreatedBy { get; set; }
 //       public double LocationX { get; set; }
 //       public double LocationY { get; set; }

        public Event(string title, string summary, int createdBy)
        {
            this.summary = summary;
            this.title = title;
            this.CreatedBy = createdBy;
 //           this.LocationX = 55.3212;
  //          this.LocationY = 44.212555;
        }
    }
}
