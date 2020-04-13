using System;
using System.Collections.Generic;
using System.Text;

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
