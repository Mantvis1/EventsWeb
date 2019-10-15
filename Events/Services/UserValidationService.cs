using Events.Models.UserModels;

namespace Events.Services
{
    public class UserValidationService
    {
        public bool isIdNotEqualsToNull(int? id)
        {
            if (id == null)
                return false;
            return true;
        }

        public bool isUserEqualsToNull(User user)
        {
            if (user == null)
                return false;
            return true;
        }
    }
}
