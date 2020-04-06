using System;

namespace DTO.RestRequests
{
    public class EditUserRequest
    {
        public UserInfoRequest UserInfo { get; set; }
        public PasswordChangeRequest PasswordRequest { get; set; }
    }
}
