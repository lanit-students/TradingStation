using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using DTO;

namespace DatabaseServiceTests.Comparators
{
    public class UserComparer : EqualityComparer<User>
    {
        public override bool Equals([AllowNull] User x, [AllowNull] User y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;

            if (
                x.Id == y.Id
                && x.FirstName == y.FirstName
                && x.LastName == y.LastName
                && x.Email == y.Email
                && x.Birthday == y.Birthday)
                return true;
            else
                return false;
        }

        public override int GetHashCode([DisallowNull] User obj)
        {
            return (obj.Id.ToString()
                + obj.FirstName
                + obj.LastName
                + obj.Email
                + obj.Birthday.ToString())
                .GetHashCode();
        }
    }
}
