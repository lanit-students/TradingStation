namespace DTO
{
    public class UserEmailPassword
    {
        UserEmailPassword() { }
        public string Email { get; set; }
        public string Password { get; set; }

        public UserEmailPassword(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
