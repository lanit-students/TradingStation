using System;

namespace DTO.RestRequests
{
    public class UserInfoRequest
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public byte[] Avatar { get; set; }
        public string AvatarExtension { get; set; }
    }
}
