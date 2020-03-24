using System;

namespace DTO
{
    public class User
    {
        public Guid Id { get; }
        public string Login { get; }
        public string Email { get; set; }
        public string PasswordHash { get; }
    }
}
