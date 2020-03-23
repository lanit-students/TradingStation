namespace DTO
{
    /// <summary>
    /// Minimal model of user: contains email and password
    /// </summary>
    public class UserCredential
    {
        public UserCredential(string email, string password)
        {
            Email = email;
            Password = password;
        }
        public string Email { get; set; }
        public string Password { get; }
    }
}
