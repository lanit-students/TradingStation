using System;

namespace DTO
{
    public class User
    {
        public User (string email, string password, Guid id)
        {
            Email = email;
            Password = password;
            Id = id;
        }
        public Guid Id { get; }
        public string Login { get; }
        public string Email { get; set; }
        public string Password { get; }
    }
}
