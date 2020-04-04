using System.Net.Mail;

using Kernel.CustomExceptions;
using Kernel.Properties;

namespace Kernel
{
    public static class CommonValidations
    {
        public static void ValidateId(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException(Errors.IdCanNotBeLessThanZero);
            }
        }

        public static void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                throw new BadRequestException();

            try
            {
                var addr = new MailAddress(email);
            }
            catch
            {
                throw new BadRequestException();
            }
        }

    }
}
