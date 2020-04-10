using System;
using System.Collections.Generic;
using System.Text;
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
                && String.CompareOrdinal(x.TypeAvatar, y.TypeAvatar) == 0)
                return true;
            else
                return false;
        }

        public override int GetHashCode(UserAvatar obj)
        {
            return (obj.Id.ToString()
                    + obj.Avatar
                    + obj.TypeAvatar
                    + obj.GetType().ToString()).
                GetHashCode();
        }
    }
}
