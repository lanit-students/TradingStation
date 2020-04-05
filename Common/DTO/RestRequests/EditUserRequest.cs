using System;

namespace DTO.RestRequests
{
    public class EditUserRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}
