using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using DataBaseService.Database.Models;

namespace DatabaseServiceTests.Comparators
{
    class DbUserCredentialsComparer : EqualityComparer<DbUserCredential>
    {
        public override bool Equals([AllowNull] DbUserCredential x, [AllowNull] DbUserCredential y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;

            if (
                x.Id == y.Id
                && x.UserId == y.UserId
                && x.Email == y.Email
                && x.PasswordHash == y.PasswordHash
                && x.IsActive == y.IsActive)
                return true;
            else
                return false;
        }

        public override int GetHashCode([DisallowNull] DbUserCredential obj)
        {
            return (obj.Id.ToString()
                + obj.UserId.ToString()
                + obj.Email
                + obj.PasswordHash
                + obj.IsActive.ToString()
                + obj.GetType().ToString()).
                GetHashCode();
        }
    }
}
