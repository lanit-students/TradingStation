namespace DTO
{
    public class UserEmailPassword
    {
        public string Email { get; }
        public string Password { get; }

        public UserEmailPassword(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
