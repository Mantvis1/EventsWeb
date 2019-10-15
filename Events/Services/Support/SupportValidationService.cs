using Events.Models;

namespace Events.Services
{
    public class SupportValidationService
    {
        public bool ifObejectIsNull(Support support)
        {
            if (support == null)
                return false;
            return true;
        }

        public bool isIdEqualToNull(int? id)
        {
            if (id == null)
                return false;
            return false;
        }

        public bool isSupportFromFilled(int? writenBy, string title, string summary)
        {
            if (writenBy != null && title != null && summary != null && title.Length != 0 
                && summary.Length != 0 && writenBy.Value.GetType().Equals(typeof(int)))
                return true;
            return false;
        }

        public bool textFieldValidation(string text)
        {
            if (text != null && text.Length != 0)
                return true;
            return false;
        }

        public bool creatorValidation(int? createdBy)
        {
            if (createdBy == null || !createdBy.Value.GetType().Equals(typeof(int)))
                return false;
            return true;
        }
    }
}
