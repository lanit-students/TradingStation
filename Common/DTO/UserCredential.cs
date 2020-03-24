namespace DTO
{
    /// <summary>
    /// Minimal model of user: contains email and password
    /// </summary>
    public class UserCredential
    {
        public string Email { get; }
        public string PasswordHash { get; }

        public UserCredential(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
