using Events.Models;

namespace Events.Services
{
    public class ErrorService
    {
        private static Error error = null;

        public static Error GetError(string message)
        {
            if (error == null)
            {
                return new Error(message);
            }
            else
            {
                error.message = message;
            }
            return error;
        }
    }
}
