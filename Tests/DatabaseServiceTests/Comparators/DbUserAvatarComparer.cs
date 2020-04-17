using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
                && x.UserId == y.UserId
                && String.CompareOrdinal(x.AvatarExtension, y.AvatarExtension) == 0)
                return true;
            else
                return false;
        }

        public override int GetHashCode([DisallowNull] DbUsersAvatars obj)
        {
            return (obj.Id.ToString()
                    + obj.UserId
                    + obj.Avatar
                    + obj.AvatarExtension
                    + obj.GetType().ToString()).
                GetHashCode();
        }
    }
}
