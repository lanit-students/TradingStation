using System;

namespace DTO.RestRequests
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public byte[] Avatar { get; set; }
        public string AvatarExtension { get; set; }
    }
}
