namespace Events.Constants
{
    public class Banned
    {
        private static bool isBanned = true;

        public static bool userIsBaned()
        {
            return isBanned;
        }

        public static bool userIsNotBaned()
        {
            return !isBanned;
        }
    }
}
