namespace Events.Models.UserModels
{
    public class UserRegisterModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public UserRegisterModel(string name, string password, string email)
        {
            Name = name;
            Password = password;
            Email = email;
        }
    }
}
