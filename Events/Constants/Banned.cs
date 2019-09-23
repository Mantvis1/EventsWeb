namespace Events.Constants
{
    public class Banned
    {
        private bool isBanned = false;

        public bool userIsBaned()
        {
            return isBanned;
        }

        public bool userIsNotBaned()
        {
            return !isBanned;
        }
    }
}
