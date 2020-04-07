using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using DTO;

namespace DatabaseServiceTests.Comparators
{
    public class UserCredentialsComparer : EqualityComparer<UserCredential>
    {
        public override bool Equals([AllowNull] UserCredential x, [AllowNull] UserCredential y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;

            if (
                x.Id == y.Id
                && x.Email == y.Email
                && x.PasswordHash == y.PasswordHash
                && x.UserId == y.UserId)
                return true;
            else
                return false;
        }

        public override int GetHashCode([DisallowNull] UserCredential obj)
        {
            return (obj.Id.ToString()
                + obj.UserId.ToString()
                + obj.Email
                + obj.PasswordHash)
                .GetHashCode();
        }
    }
}
