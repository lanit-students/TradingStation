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
        
        public static void ValidateEmail(string email)
        {

            var addr = new MailAddress(email);             
            if (addr.Address != email)
            {
                throw new Exception(Errors.EmailCanNotBeNullEmptyOrNotEmailFormat);
            }  
        }

    }
}
