namespace DTO
{
    public class UserEmailPassword
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public UserEmailPassword() {}

        public UserEmailPassword(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
