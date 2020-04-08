using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using DataBaseService.Database.Models;

namespace DatabaseServiceTests.Comparators
{
    class DbUserComparer : EqualityComparer<DbUser>
    {
        public override bool Equals([AllowNull] DbUser x, [AllowNull] DbUser y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;

            if (
                x.Id == y.Id
                && x.FirstName == y.FirstName
                && x.LastName == y.LastName
                && x.Birthday == y.Birthday)
                return true;
            else
                return false;
        }

        public override int GetHashCode([DisallowNull] DbUser obj)
        {
            return (obj.Id.ToString()
                + obj.FirstName
                + obj.LastName
                + obj.Birthday
                + obj.GetType().ToString())
                .GetHashCode();
        }
    }
}
