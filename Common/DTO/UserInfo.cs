using System;

namespace DTO
{
    public class UserInfo
    {
        public string Name { get; }
        public string Surname { get; }
        public string Email { get; }
        public DateTime DateOfBirth { get; }

        public UserInfo(string name, string surname, string email, DateTime dateOfBirth)
        {
            Name = name;
            Surname = surname;
            Email = email;
            DateOfBirth = dateOfBirth;
        }
    }
}
