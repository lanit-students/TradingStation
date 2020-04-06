using Kernel.CustomExceptions;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Kernel
{
    public static class ShaHash
    {
        public static string GetPasswordHash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new BadRequestException();

            using var sha1 = new SHA1Managed();

            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password));

            var stringBuilder = new StringBuilder(hash.Length * 2);
            stringBuilder.Append(string.Join(null, hash.Select(b => b.ToString("X2"))));

            return stringBuilder.ToString();
        }
    }
}
