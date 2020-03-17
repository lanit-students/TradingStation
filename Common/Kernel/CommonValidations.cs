using Kernel.Properties;
using System;

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
