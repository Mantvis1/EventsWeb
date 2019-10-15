namespace Events.Models
{
    public class EventUpdateFrom
    {
        public EventUpdateFrom(string title, string summary)
        {
            this.title = title;
            this.summary = summary;
        }

        public string title { get; set; }
        public string summary { get; set; }
    }
}
