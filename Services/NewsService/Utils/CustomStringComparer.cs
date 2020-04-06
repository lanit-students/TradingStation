using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NewsService.Utils
{
    public class CustomStringComparer : EqualityComparer<string>
    {
        public override bool Equals([AllowNull] string x, [AllowNull] string y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;

            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode([DisallowNull] string obj)
        {
            return obj.GetHashCode();
        }
    }
}
