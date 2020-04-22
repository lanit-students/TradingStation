using System;
using System.Collections.Generic;
using DTO;

namespace DatabaseServiceTests.Comparators
{
    class UserAvatarComparer : EqualityComparer<UserAvatar>
    {
        public override bool Equals(UserAvatar x, UserAvatar y)
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

        public override int GetHashCode(UserAvatar obj)
        {
            return (obj.Id.ToString()
                    + obj.Avatar
                    + obj.UserId
                    + obj.AvatarExtension
                    + obj.GetType().ToString()).
                GetHashCode();
        }
    }
}
