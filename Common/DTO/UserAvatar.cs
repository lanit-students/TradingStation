using System;

namespace DTO
{
    public class UserAvatar
    {
        public Guid Id { get; set; }
        public byte[] Avatar { get; set; }
        public string AvatarExtension { get; set; }
        public Guid UserId { get; set; }
    }
}
