using Events.Models;

namespace Events.Services
{
    public class EventValidationService
    {
        public bool isIdNotEqualsToNull(int? id)
        {
            if (id == null)
                return false;
            return true;
        }

        public bool isUserEqualsToNull(Event @event)
        {
            if (@event == null)
                return false;
            return true;
        }

        public bool newEventCreation(string title, string summary, int? createdBy)
        {
            if (title == null || summary == null || createdBy == null || !createdBy.Value.GetType().Equals(typeof(int)))
                return false;
            return true;
        }

        public bool textFieldValidation(string text)
        {
            if (text != null && text.Length != 0)
                return true;
            return false;
        }
    }
}
