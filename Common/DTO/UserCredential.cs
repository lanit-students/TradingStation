using System;

namespace DTO
{
    public class UserCredential
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
