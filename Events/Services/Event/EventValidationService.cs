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
    }
}
