using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using DataBaseService.Database.Models;

namespace DatabaseServiceTests.Comparators
{
    class DbUserAvatarComparer : EqualityComparer<DbUsersAvatars>
    {
        public override bool Equals([AllowNull] DbUsersAvatars x, [AllowNull] DbUsersAvatars y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;

            if (
                x.Id == y.Id
                && x.Avatar == y.Avatar
                && String.CompareOrdinal(x.TypeAvatar, y.TypeAvatar) == 0)
                return true;
            else
                return false;
        }

        public override int GetHashCode([DisallowNull] DbUsersAvatars obj)
        {
            return (obj.Id.ToString()
                    + obj.Avatar
                    + obj.TypeAvatar
                    + obj.GetType().ToString()).
                GetHashCode();
        }
    }
}
