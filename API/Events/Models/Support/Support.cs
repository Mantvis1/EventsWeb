using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Models
{
    [Table("Support")]
    public class Support
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public int WritenBy { get; set; }
        public string Solution { get; set; }
        public int? SolvedBy { get; set; }

        public Support(string title, string summary, int writenBy, string soliution, int solvedBy)
        {
            Title = title;
            Summary = summary;
            WritenBy = writenBy;
            Solution = soliution;
            SolvedBy = solvedBy;
        }
        
        public Support(string title, string summary, int writenBy)
        {
            Title = title;
            Summary = summary;
            WritenBy = writenBy;
            Solution = "";
            SolvedBy = null;
        }
    }
}
