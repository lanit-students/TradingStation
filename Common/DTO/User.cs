using System;

namespace DTO
{
    public class User : IEquatable<User>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public bool Equals(User other)
        {
            if (other == null)
                return false;

            if (
                Id == other.Id
                && Email == other.Email
                && DateTime.Equals(Birthday, other.Birthday))
                return true;
            else
                return false;

        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            User userObj = obj as User;
            if (userObj == null)
                return false;
            else
                return Equals(userObj);
        }

        public override int GetHashCode()
        {
            return (Id.ToString()
                + Email
                + Birthday.ToString()
                + FirstName
                + LastName
                + Birthday.ToString()
                + GetType().ToString())
                .GetHashCode();
        }
    }
}
