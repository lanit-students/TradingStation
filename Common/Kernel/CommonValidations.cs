using System;
using System.Net.Mail;
using Kernel.Properties;

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
        
        public static bool ValidateEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
