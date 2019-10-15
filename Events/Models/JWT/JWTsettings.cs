namespace Events.Models
{
    public class JWTsettings
    {
        private string secret;

        public string GetSecret()
        {
            return secret;
        }

        public void SetSecret(string value)
        {
            secret = value;
        }
    }
}
