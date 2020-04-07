using System;

namespace DTO
{
    public class UserCredential : IEquatable<UserCredential>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public bool Equals(UserCredential other)
        {
            if (other == null)
                return false;

            if (Id == other.Id
                && UserId == other.UserId
                && Email == other.Email
                && PasswordHash == other.PasswordHash)
                return true;
            else
                return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            UserCredential credentialObj = obj as UserCredential;
            if (credentialObj == null)
                return false;
            else
                return Equals(credentialObj);
        }

        public override int GetHashCode()
        {
            return (Id.ToString()
                + UserId.ToString()
                + Email
                + PasswordHash
                + GetType().ToString())
                .GetHashCode();
        }
    }
}
