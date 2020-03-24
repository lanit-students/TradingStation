using System;

namespace DataBaseService.DbModels
{
    public class DbUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
