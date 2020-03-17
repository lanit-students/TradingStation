namespace DTO
{
    public class User
    {
        public User (string email, string password)
        {
            Email = email;
            Password = password;
        }
        public int Id { get; }
        public string Login { get; }
        public string Email { get; set; }
        public string Password { get; }
    }
}
