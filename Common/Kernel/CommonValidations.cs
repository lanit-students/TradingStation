using Kernel.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kernel
{
    public static class CommonValidations
    {
        public static void ValidateId(int id)
        {
            if (id <= 0)
            {
                throw new Exception(Errors.IdCanNotBeLessThanZero);
            }
        }
    }
}
